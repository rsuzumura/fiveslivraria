<%@taglib uri="http://www.springframework.org/tags/form" prefix="forms" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Cadastro de usuários</title>
</head>
<body>
	<h1>Cadastro de usuário</h1>
		<form action="cadastraUsuario" method="POST">
		<table>
			<tr>
				<td>Nome:</td>
				<td><input name="nmUsuario" type="text"></td>
			</tr>
			<tr>
				<td>Endereço:</td>
				<td><input name="dsEndereco" type="text"></td>
			</tr>
			<tr>
				<td>Usuário:</td>
				<td><input name="dsLogin" type="text"></td>
			</tr>
			<tr>
				<td>Senha:</td>
				<td><input name="senha" type="text"></td>
			</tr>
			<tr>
				<td>Pergunta secreta:</td>
				<td><input name="dsPergunta" type="text"></td>
			</tr>
			<tr>
				<td>Resposta secreta:</td>
				<td><input name="dsResposta" type="text"></td>
			</tr>
			<tr>
				<td>E-mail:</td>
				<td><input name="dsEmail" type="text"></td>
			</tr>
			<tr>
				<td>Tipo Usuario</td>
				<td><input name="tpUsuario" type="text"></td>
			</tr>
		</table>
		<input id="btnCadastrar" type="submit" value="Cadastar">
	</form>
</body>
</html>