package services;

import java.util.Enumeration;

import javax.faces.application.FacesMessage;
import javax.faces.context.FacesContext;
import javax.servlet.http.HttpSession;

public class FacesTools {
	
	public final static void addMessage(String msg) {
		FacesContext.getCurrentInstance().addMessage(null, new FacesMessage(msg));
	}
	
	public final static void addMessage(String clientId, FacesMessage.Severity severity, String summary, String detail) {
		String d = clientId == null ? detail : '"'+ clientId + "\": " + detail;
		FacesContext.getCurrentInstance().addMessage(clientId, new FacesMessage(severity, summary, d));
	}
	
	public final static void addInfoMessage(String clientId, String detail) {
		addMessage(clientId, FacesMessage.SEVERITY_INFO, null, detail);
	}
	public final static void addInfoMessage(String detail) {
		addMessage(null, FacesMessage.SEVERITY_INFO, null, detail);
	}
	public final static void addWarnMessage(String clientId, String detail) {
		addMessage(clientId, FacesMessage.SEVERITY_WARN, null, detail);
	}
	public final static void addWarnMessage(String detail) {
		addMessage(null, FacesMessage.SEVERITY_WARN, null, detail);
	}
	public final static void addErrorMessage(String clientId, String detail) {
		addMessage(clientId, FacesMessage.SEVERITY_ERROR, null, detail);
	}
	public final static void addErrorMessage(String detail) {
		addMessage(null, FacesMessage.SEVERITY_ERROR, null, detail);
	}
	public final static void addFatalMessage(String clientId, String detail) {
		addMessage(clientId, FacesMessage.SEVERITY_FATAL, null, detail);
	}
	public final static void addFatalMessage(String detail) {
		addMessage(null, FacesMessage.SEVERITY_FATAL, null, detail);
	}
	
	@SuppressWarnings("unchecked")
	public static void invalidateSession(HttpSession session) {
//		session.invalidate();	// <- replace the rest of the code with this line when the bug gets fixed
		String[] extraAttrs = {	// Attributes which don't follow the convention of ending in "Bean"
				"userInfo",
		};
		Enumeration<String> attrs = session.getAttributeNames();
		while (attrs.hasMoreElements()) {
			String attr = attrs.nextElement();
			if (attr.endsWith("Bean"))
				session.removeAttribute(attr);
		}
		for (int i = 0; i < extraAttrs.length; i++)
			session.removeAttribute(extraAttrs[i]);		
	}
	
}
