declare @o varchar(100); set @o = 'spCadastra_usuario';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spCadastra_usuario (
	@xml xml,
	@idUsuario int output
) as
begin
	set xact_abort on;

	begin try
		begin transaction;

		insert into Usuarios (
			nmUsuario,
			dsEndereco,
			dsLogin)
		select
			x.n.value('nmUsuario[1]', 'varchar(200)'),
			x.n.value('dsEndereco[1]', 'varchar(200)'),
			x.n.value('dsLogin[1]', 'varchar(100)')
		from
			@xml.nodes('/*[1]') x(n);

		set @idUsuario = scope_identity();
		
		commit transaction;
	end try
	begin catch
		if (xact_state()) = -1
			rollback transaction;
		if (xact_state()) = 1
			commit transaction;
	end catch;
end