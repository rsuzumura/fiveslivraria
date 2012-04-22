package projeto.livraria.controller;

import javax.validation.Valid;

import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.RequestMapping;

import projeto.livraria.classes.Usuario;
import projeto.livraria.dao.CallStoredProcedure;
import projeto.livraria.dao.Parametro;

@Controller
public class UsuarioController {

	@RequestMapping("/cadastraUsuario")
	public String cadastraUsuario( Usuario usuario, BindingResult result) {

		CallStoredProcedure proc = new CallStoredProcedure();
		Parametro parametros = new Parametro();
		parametros.add("@nmUsuario", usuario.nmUsuario);
		parametros.add("@dsEndereco", usuario.dsEndereco);
		parametros.add("@dsEmail", usuario.dsEmail);
		parametros.add("@dsLogin", usuario.dsLogin);
		parametros.add("@dsPergunta", usuario.dsPergunta);
		parametros.add("@dsResposta", usuario.dsResposta);
		parametros.add("@senha", usuario.senha);
		parametros.add("@tpUsuario", usuario.tpUsuario);
		proc.executaSp("{call dbo.spCadastra_usuario(?, ?, ?, ?, ?, ?, ?, ?)}", parametros.get());
		return "index";
	}
}