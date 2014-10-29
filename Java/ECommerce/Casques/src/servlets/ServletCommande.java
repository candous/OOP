package servlets;

import java.io.IOException;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import Actions.ActionCommande;
import Actions.ActionIndex;
import beans.User;

/**
 * Servlet implementation class ServletCommande
 */
@WebServlet("/servletCommande")
public class ServletCommande extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public ServletCommande() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		
		boolean userExists=false;
		String nom=null;
			
		if(request.getSession().getAttribute("user")!=null)
		{
			userExists=true;
			User leUser=(User)request.getSession().getAttribute("user");
			ActionCommande.creerCommande(request, response,leUser);
			//ActionCommande.envoyerCourriel(request, response,leUser);
		}
		else
			ActionIndex.redirection(request, response, "login");
		
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		doGet(request, response);
	}

}
