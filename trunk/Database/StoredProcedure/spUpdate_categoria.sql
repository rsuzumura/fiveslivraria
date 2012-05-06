declare @o varchar(100); set @o = 'spUpdate_categoria';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go

create procedure spUpdate_categoria (
	@xml xml
) as
begin
	update Categoria set
		dsCategoria = x.n.value('dsCategoria[1]','varchar(100)'),
		stCategoria = x.n.value('stCategoria[1]','bit')
	from
		@xml.nodes('/*[1]') x(n)
	where
		idCategoria = x.n.value('idCategoria[1]','int');
end
