declare @o varchar(100); set @o = 'spAltera_item_carrinho';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spAltera_item_carrinho (
	@nrQtdProduto int
	,@idCarrinho	int
) as
begin
	
	
			
	update c set 
	c.nrQtdProduto = @nrQtdProduto
	,c.vlFinal = @nrQtdProduto * p.vlPreco
	from Carrinho c 
	inner join Produtos p ON
	c.idProduto = p.idProduto
	where idCarrinho = @idCarrinho
end
