declare @o varchar(100); set @o = 'spRemove_categoria';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spRemove_categoria (
	@idCategoria int,
	@result bit output
) as
begin
	begin try
		delete from
			Categoria
		where
			idCategoria = @idCategoria;
		set @result = 1;
	end try
	begin catch
		set @result = 0;
	end catch
end
