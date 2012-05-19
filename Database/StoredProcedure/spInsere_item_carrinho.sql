declare @o varchar(100); set @o = 'spInsere_item_carrinho';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spInsere_item_carrinho (
	@idProduto int
	,@idUsuario	int
) as
begin
	
	declare @idCliente int
	
	select 
		@idCliente = idCliente 
	from 
		Cliente c
		inner join Usuarios u ON c.idUsuario = u.idUsuario
	where
		c.idUsuario = @idUsuario
	
	insert into Carrinho(
	idProduto
	,idCliente
	,nrQtdProduto
	,vlFinal
	,dvStatus
	)
	select @idProduto
	,@idCliente
	,1
	,vlPreco
	,'C'
	from Produtos
	where idProduto = @idProduto
	
	
	
end
