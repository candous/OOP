package pkgListeners;

import javax.faces.application.NavigationHandler;
import javax.faces.context.FacesContext;
import javax.faces.event.PhaseEvent;
import javax.faces.event.PhaseId;
import javax.faces.event.PhaseListener;
import javax.servlet.http.HttpSession;

public class PhaseListeners implements PhaseListener {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	@Override
	public void afterPhase(PhaseEvent event) {
		// TODO Auto-generated method stub
		
		FacesContext facesContext=event.getFacesContext();
		
		String currentPage=facesContext.getViewRoot().getViewId();
		
		boolean isLoginPage=(currentPage.lastIndexOf("index.xhtml")>-1);
		
		HttpSession session=(HttpSession)facesContext.getExternalContext().getSession(false);
		
		if (session==null) {
			
			NavigationHandler nh=facesContext.getApplication().getNavigationHandler();
			nh.handleNavigation(facesContext, null, "index");
		}
		else{
			
			Object currentUser=session.getAttribute("user");
			
			if (!isLoginPage && (currentUser==null || currentUser=="")) {
				NavigationHandler nh=facesContext.getApplication().getNavigationHandler();
				nh.handleNavigation(facesContext, null, "index");
			}
		}
	}

	@Override
	public void beforePhase(PhaseEvent arg0) {
		// TODO Auto-generated method stub
	}

	@Override
	public PhaseId getPhaseId() {
		// TODO Auto-generated method stub
		return PhaseId.RESTORE_VIEW;
	}
}