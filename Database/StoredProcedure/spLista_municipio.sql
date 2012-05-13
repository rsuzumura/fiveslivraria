declare @o varchar(100); set @o = 'spLista_municipio';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_municipio (
	@idEstado int
)
as
begin	
	select 			
		idMunicipio,
		nmMunicipio,
		idEstado
	from 
		Municipio
	where
		idEstado = @idEstado
	for xml auto, elements, root('MunicipioCollection')
end
