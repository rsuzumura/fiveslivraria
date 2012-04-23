<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<script type="text/javascript" src="resources/js/jquery.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Lista de usuarios</title>
</head>
<body>
	<script type="text/javascript">
	function remove(tr, id) {
		$.post("removeUsuario", {'id' : id}, function() {
			$("#"+tr).hide("slow");
		});
	}
	
	function mostraForm(idUsuario, nmUsuario, dsEndereco, dsLogin, senha, dsPergunta, dsResposta, dsEmail, tpUsuario ) {
		$("#idUsuario").val(idUsuario);
		$("#nmUsuario").val(nmUsuario);
		$("#dsEndereco").val(dsEndereco);
		$("#dsLogin").val(dsLogin);
		$("#senha").val(senha);
		$("#dsPergunta").val(dsPergunta);
		$("#dsResposta").val(dsResposta);
		$("#dsEmail").val(dsEmail);
		$("#tpUsuario").val(tpUsuario);
		$("#formulario").show("slow");
	}
	
	function escondeForm(){
		$("#formulario").hide("slow");
	}
	</script>

	<h1>Alteração de Usuarios</h1>
	<div id="formulario" style="display: none">
		<form action="alteraUsuario" method="POST">
			<table>
				<tr>
					<td>ID:</td>
					<td><input name="idUsuario" type="text" id="idUsuario"
						></td>
				</tr>
				<tr>
					<td>Nome:</td>
					<td><input name="nmUsuario" type="text" id="nmUsuario"></td>
				</tr>
				<tr>
					<td>Endereço:</td>
					<td><input name="dsEndereco" type="text" id="dsEndereco"></td>
				</tr>
				<tr>
					<td>Usuário:</td>
					<td><input name="dsLogin" type="text" id="dsLogin"></td>
				</tr>
				<tr>
					<td>Senha:</td>
					<td><input name="senha" type="text" id="senha"></td>
				</tr>
				<tr>
					<td>Pergunta secreta:</td>
					<td><input name="dsPergunta" type="text" id="dsPergunta"></td>
				</tr>
				<tr>
					<td>Resposta secreta:</td>
					<td><input name="dsResposta" type="text" id="dsResposta"></td>
				</tr>
				<tr>
					<td>E-mail:</td>
					<td><input name="dsEmail" type="text" id="dsEmail"></td>
				</tr>
				<tr>
					<td>Tipo Usuario</td>
					<td><input name="tpUsuario" type="text" id="tpUsuario"></td>
				</tr>
			</table>
			<input id="btnSalvar" type="submit" value="Salvar"> <input
				id="btnCancelar" type="button" value="Cancelar"
				onclick="escondeForm()">
		</form>
		<br /> <br /> <br />
	</div>

	<table id="teste">
		<tr>
			<th></th>
			<th></th>
			<th>Id</th>
			<th>Nome</th>
			<th>Usuario</th>
			<th>Tipo de Usuario</th>
		</tr>
		<c:forEach items="${usuarios}" var="usuario" varStatus="tr">
			<tr id="${tr.count}">
				<td><a href="#"
					onclick="mostraForm(${usuario.idUsuario}, '${usuario.nmUsuario}', '${usuario.dsEndereco}', '${usuario.dsLogin}'
				, '${usuario.senha}', '${usuario.dsPergunta}', '${usuario.dsResposta}', '${usuario.dsEmail}', '${usuario.tpUsuario}')">Alterar</a></td>
				<td><a href="#"
					onclick="remove('${tr.count}' , '${usuario.idUsuario}')">Remover</a></td>
				<td>${usuario.idUsuario}</td>
				<td>${usuario.nmUsuario}</td>
				<td>${usuario.dsLogin}</td>
				<td>${usuario.tpUsuario}</td>
			</tr>
		</c:forEach>
	</table>

</body>
</html>