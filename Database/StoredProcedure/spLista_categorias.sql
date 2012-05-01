declare @o varchar(100); set @o = 'spLista_categorias';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_categorias(
@nada int = null
) 
as
begin
	
	select 			
			 idCategoria
			,dsCategoria
		from 
			Categoria
			
			
	for xml auto, elements, root('ListaCategoria')
end