declare @o varchar(100); set @o = 'spLista_estado';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_estado
as
begin	
	select 			
		idEstado,
		nmEstado,
		siglaEstado
	from 
		Estado
	for xml auto, elements, root('EstadoCollection')
end