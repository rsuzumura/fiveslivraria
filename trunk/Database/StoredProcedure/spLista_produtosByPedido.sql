declare @o varchar(100); set @o = 'spLista_produtosByPedido';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_produtosByPedido (
	@idPedido int
) as
begin
	select
		[Produto].*
	from (
		select 			
			 p.idProduto [idProduto]
			,nmTitulo
			,nmTituloOriginal
			,dsProduto
			,ISBN
			,dsAutores
			,nmEditora
			,nrAno
			,dsEdicao
			,ip.qtdProduto [qtdProduto]
			,nmImagem
			,vlPreco
			,ip.vlFinal [vlFinal]
			,cat.dsCategoria [dsCategoria]
			,ent.dsEndereco [EnderecoEntrega]
			,cob.dsEndereco [EnderecoCobranca]
		from
			Pedido ped
			inner join ItemPedido ip on ped.idPedido = ip.idPedido
			inner join Produtos p on p.idProduto = ip.idProduto
			left Join Categoria cat on p.idCategoria = cat.idCategoria
			left join Endereco ent on ent.idEndereco = ped.idEnderecoEntrega
			left join Endereco cob on cob.idEndereco = ped.idEnderecoCobranca
		where
			ped.idPedido = @idPedido
	) [Produto]
	for xml auto, elements, root('ListaProduto')
end
