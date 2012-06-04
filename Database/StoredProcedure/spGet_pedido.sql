declare @o varchar(100); set @o = 'spGet_pedido';
if object_id(@o, 'P') is not null begin
	declare @d nvarchar(250); set @d = 'drop procedure ' + @o;
	execute sp_executesql @d;
end;
go
create procedure spGet_pedido (
	@idPedido int
) as
begin
	select
		[Pedido].*
	from (
		select
			idPedido,
			p.idCliente [idCliente],
			dtPedido,
			idUsuario,
			dtEntregaPrevista,
			dtEntregaReal,
			idEnderecoEntrega,
			ent.dsEndereco [EnderecoEntrega],
			idEnderecoCobranca,
			cob.dsEndereco [EnderecoCobranca],
			tpPagamento,
			nrParcelas
		from
			Pedido p
			left join Endereco ent on ent.idEndereco = p.idEnderecoEntrega
			left join Endereco cob on cob.idEndereco = p.idEnderecoCobranca
		where
			idPedido = @idPedido
		) [Pedido]
	for xml auto, elements
end
