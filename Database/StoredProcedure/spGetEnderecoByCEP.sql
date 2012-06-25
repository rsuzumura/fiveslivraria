declare @o varchar(100); set @o = 'spGetEnderecoByCEP';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spGetEnderecoByCEP (
	@cep varchar(10)
)
as
begin
	select
		[CEP].*
	from (
		select
			cep,
			endereco,
			bairro,
			cidade,
			idEstado
		from
			tend_endereco
			inner join tend_bairro on tend_bairro.id_bairro = tend_endereco.id_bairro
			inner join tend_cidade on tend_bairro.id_cidade = tend_cidade.id_cidade
			inner join Estado on siglaEstado = uf
		 where 
			cep = @cep
	) [CEP]
	for xml auto, elements
end
go
