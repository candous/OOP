package servlets;

import java.io.IOException;
import java.math.BigDecimal;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import beans.Produit;
import Actions.ActionGererPanier;

/**
 * Servlet implementation class ServletAjouterPanier
 */
@WebServlet("/servletGererPanier")
public class ServletGererPanier extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public ServletGererPanier() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		
		int id_produit=Integer.parseInt(request.getParameter("id"));
		String nom=request.getParameter("nom");
		String description=request.getParameter("description");
		BigDecimal cout=BigDecimal.valueOf(Double.parseDouble(request.getParameter("cout")));
		BigDecimal prix=BigDecimal.valueOf(Double.parseDouble(request.getParameter("prix")));
		int stock=Integer.parseInt(request.getParameter("stock"));
		String url=request.getParameter("url");
		int categorie=Integer.parseInt(request.getParameter("categorie"));
		
		int quantite=Integer.parseInt(request.getParameter("quantite"));
		
		//creation du produit pour ajouter au panier
		Produit produit=new Produit();
		
		produit.setId(id_produit);
		produit.setNom(nom);
		produit.setDescriptionProduit(description);
		produit.setPrixProduitCoutant(cout);
		produit.setPrixProduitVendu(prix);
		produit.setQteProduit(stock);
		produit.setUrl(url);
		produit.setNoCategorie(categorie);
		
		String action=String.valueOf(request.getParameter("bouton"));
		
		switch(action)
		{
			case "MAJ": ActionGererPanier.majProduitPanier(request, response, quantite, produit);
				break;
			case "Delete": ActionGererPanier.deleteProduitPanier(request, response, produit);
				break;
			default:ActionGererPanier.ajouterProduitPanier(request, response, quantite, produit);
				break;
		}
		
		
		
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		doGet(request, response);
	}

}
