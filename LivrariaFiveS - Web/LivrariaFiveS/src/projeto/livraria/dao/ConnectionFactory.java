package projeto.livraria.dao;

import java.sql.Connection;
import java.sql.DriverManager;

public class ConnectionFactory {

	private final String url = "jdbc:jtds:sqlserver://fiveslivraria.no-ip.org:1433//FivesLivraria";
	private final String serverName= "fiveslivraria.no-ip.org";
	private final String portNumber = "1433";
	private final String databaseName= "livraria";
	private final String userName = "fatec";
	private final String password = "f1v&$l1vr@r1@";
	// Informs the driver to use server a side-cursor, 
	// which permits more than one active statement 
	// on a connection.
	private final String selectMethod = "cursor"; 

	public Connection getConnection() {		
		try {
			Class.forName("net.sourceforge.jtds.jdbc.Driver");
			return DriverManager.getConnection(getConnectionUrl(), userName, password);
		} catch(Exception e) {
			throw new RuntimeException(e);
		}
	}	 

	private String getConnectionUrl(){
		return url;
	}

}
