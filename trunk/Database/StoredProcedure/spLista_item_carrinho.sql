declare @o varchar(100); set @o = 'spLista_item_carrinho';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spLista_item_carrinho (
	 @idUsuario int
	,@vlTotal decimal(10,2) = null out
) as
begin
	
	declare @idCliente int
	
	select 
		@idCliente = idCliente 
	from 
		Cliente c
		inner join Usuarios u ON c.idUsuario = u.idUsuario
	where
		c.idUsuario = @idUsuario;
	
	select @vlTotal = SUM(vlFinal) 
	from Carrinho
	where idCliente = @idCliente
		and dvStatus = 'C'
	
	select
		[Carrinho].*
	from (
		select 			
			 p.idProduto			[idProduto]
			,p.nmTitulo				[nmTitulo]
			,p.nmTituloOriginal		[nmTituloOriginal]
			,p.dsProduto			[dsProduto]
			,p.ISBN					[ISBN]
			,p.dsAutores			[dsAutores]
			,p.nmEditora			[nmEditora]
			,p.nrAno				[nrAno]
			,p.dsEdicao				[dsEdicao]
			,p.qtdProduto			[qtdProduto]
			,p.nmImagem				[nmImagem]
			,p.vlPreco				[vlPreco]
			,c.idCarrinho
			,c.idCliente			
			,c.nrQtdProduto
			,c.vlFinal
			,c.dvStatus
			--,cat.dsCategoria [dsCategoria]
		from 
			Carrinho c
			Inner Join Produtos p ON
			c.idProduto = p.idProduto			
		where
			c.idCliente = @idCliente
			and dvStatus = 'C'
	) [Carrinho]
	
	for xml auto, elements, root('ListaCarrinho')
end
