declare @o varchar(100); set @o = 'spGet_cliente';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spGet_cliente (
	@idUsuario int
) as
begin
	select
		idCliente,
		idUsuario,
		nmCliente
	from
		Cliente
	where
		idUsuario = @idUsuario
	for xml auto, elements
end
