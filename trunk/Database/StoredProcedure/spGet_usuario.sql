declare @o varchar(100); set @o = 'spGet_usuario';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spGet_usuario (
	@dsLogin varchar(100)
) as
begin
	select
		idUsuario,
		nmUsuario,
		dsEndereco,
		dsLogin
	from
		Usuarios [Usuario]
	for xml auto, elements
end