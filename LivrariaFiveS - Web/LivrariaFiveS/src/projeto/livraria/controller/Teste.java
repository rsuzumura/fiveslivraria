package projeto.livraria.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class Teste {
	
	@RequestMapping("/olaMundoSpring")
	public String execute() {
	System.out.println("Executando a lógica com Spring MVC");
	return "index";
	}
	

}
