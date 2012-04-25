package projeto.livraria.controller;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import javax.servlet.http.HttpServletResponse;
import javax.validation.Valid;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;
import projeto.livraria.classes.Usuario;
import projeto.livraria.dao.CallStoredProcedure;
import projeto.livraria.dao.Parametro;

@Controller
public class UsuarioController {

	@RequestMapping("cadastraUsuario")
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
		return "redirect:listaUsuarios";
	}


	@RequestMapping("listaUsuarios")
	public String lista(Model model) {
		CallStoredProcedure proc = new CallStoredProcedure();
		Parametro parametros = new Parametro();
		List<Usuario> usuarios = new ArrayList<Usuario>();
		ResultSet rs = proc.executaSp("{call dbo.spLista_usuario()}", parametros.get());
		try {
			while (rs.next()) {
				// criando o objeto Contato
				Usuario usuario = new Usuario();
				usuario.setIdUsuario(rs.getInt("idUsuario"));
				usuario.setNmUsuario(rs.getString("nmUsuario"));
				usuario.setDsLogin(rs.getString("dsLogin"));
				usuario.setTpUsuario(rs.getInt("tpUsuario"));
				usuario.setDsEndereco(rs.getString("dsEndereco"));
				usuario.setDsEmail(rs.getString("dsEmail"));
				usuario.setDsPergunta(rs.getString("dsPergunta"));
				usuario.setDsResposta(rs.getInt("dsResposta"));
				usuario.setSenha(rs.getString("senha"));
				// adicionando o objeto à lista
				usuarios.add(usuario);				
			}
			rs.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
		model.addAttribute("usuarios", usuarios);
		return "/formListaUsuarios";
	}
	
	@RequestMapping("removeUsuario")
	public void removeUsuario(int id, HttpServletResponse response) {
		CallStoredProcedure proc = new CallStoredProcedure();
		Parametro parametros = new Parametro();
		parametros.add("@idUsuario", id);		
		ResultSet rs = proc.executaSp("{call dbo.spRemove_usuario(?)}", parametros.get());
		response.setStatus(200);
	}
	
	@RequestMapping("alteraUsuario")
	public String alteraUsuario(Usuario usuario, BindingResult result) {

		CallStoredProcedure proc = new CallStoredProcedure();
		Parametro parametros = new Parametro();
		parametros.add("@idUsuario", usuario.idUsuario);
		parametros.add("@nmUsuario", usuario.nmUsuario);
		parametros.add("@dsEndereco", usuario.dsEndereco);
		parametros.add("@dsEmail", usuario.dsEmail);
		parametros.add("@dsLogin", usuario.dsLogin);
		parametros.add("@dsPergunta", usuario.dsPergunta);
		parametros.add("@dsResposta", usuario.dsResposta);
		parametros.add("@senha", usuario.senha);
		parametros.add("@tpUsuario", usuario.tpUsuario);
		ResultSet rs = proc.executaSp("{call dbo.spAltera_usuario(?, ?, ?, ?, ?, ?, ?, ?, ?)}", parametros.get());		
		return "redirect:listaUsuarios";
	}
}
