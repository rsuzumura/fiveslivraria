declare @o varchar(100); set @o = 'spLista_endereco';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_endereco (
	@idCliente int
) as
begin
	select
		idEndereco,
		dsEndereco,
		nrEndereco,
		compEndereco,
		cep,
		dsBairro,
		idMunicipio,
		idCliente
	from
		Endereco
	where
		idCliente = @idCliente
	for xml auto, elements, root('EnderecoCollection')
end
