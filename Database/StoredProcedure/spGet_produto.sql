declare @o varchar(100); set @o = 'spGet_produto';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spGet_produto (
	@idProduto int
) as
begin
	select
		[Produto].*
	from (
		select 			
			 idProduto
			,nmTitulo
			,nmTituloOriginal
			,dsProduto
			,ISBN
			,dsAutores
			,nmEditora
			,nrAno
			,dsEdicao
			,qtdProduto
			,nmImagem
			,vlPreco
			,p.idCategoria [idCategoria]
			,cat.dsCategoria [dsCategoria]
		from 
			Produtos p
			left Join Categoria cat on
			p.idCategoria = cat.idCategoria	
		where
			idProduto = @idProduto
	) [Produto]
	for xml auto, elements
end
go


