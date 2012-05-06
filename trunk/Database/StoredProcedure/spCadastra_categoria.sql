declare @o varchar(100); set @o = 'spCadastra_categoria';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spCadastra_categoria (
	@xml xml
) as
begin
	insert into Categoria (
		dsCategoria,
		stCategoria
	)
	select
		x.n.value('dsCategoria[1]','varchar(100)'),
		x.n.value('stCategoria[1]','bit')
	from
		@xml.nodes('/*[1]') x(n);
end
