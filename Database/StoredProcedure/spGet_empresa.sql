declare @o varchar(100); set @o = 'spGet_empresa';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spGet_empresa (
	@idUsuario int
) as
begin
	select
		[Empresa].*
	from (
		select
			c.idCliente [idCliente],
			nmCliente,
			idUsuario,
			nmRazaoSocial,
			cnpj,
			inscricaoEstadual,
			inscricaoMunicipal
		from
			Cliente c
			inner join Empresa e on e.idCliente = c.idCliente
		where
			idUsuario = @idUsuario
		) [Empresa]
	for xml auto, elements
end
