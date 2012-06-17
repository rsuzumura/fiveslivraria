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
	
	if exists (select 1 
				from Carrinho 
				where idCliente = @idCliente
				and idProduto = @idProduto
				and dvStatus = 'C')
	begin
		update c set 
		nrQtdProduto = nrQtdProduto + 1 
		,c.vlFinal = vlFinal + p.vlPreco
		from Carrinho c
		inner join Produtos p on
		p.idProduto = c.idProduto
		where c.idCliente = @idCliente
		and c.idProduto = @idProduto
		and c.dvStatus = 'C'
	end
	else
	begin
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
	
end
