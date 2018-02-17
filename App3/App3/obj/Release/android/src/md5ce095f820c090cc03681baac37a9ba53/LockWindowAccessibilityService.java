package md5ce095f820c090cc03681baac37a9ba53;


public class LockWindowAccessibilityService
	extends android.accessibilityservice.AccessibilityService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onKeyEvent:(Landroid/view/KeyEvent;)Z:GetOnKeyEvent_Landroid_view_KeyEvent_Handler\n" +
			"n_onAccessibilityEvent:(Landroid/view/accessibility/AccessibilityEvent;)V:GetOnAccessibilityEvent_Landroid_view_accessibility_AccessibilityEvent_Handler\n" +
			"n_onInterrupt:()V:GetOnInterruptHandler\n" +
			"";
		mono.android.Runtime.register ("App3.utils.LockWindowAccessibilityService, App3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LockWindowAccessibilityService.class, __md_methods);
	}


	public LockWindowAccessibilityService ()
	{
		super ();
		if (getClass () == LockWindowAccessibilityService.class)
			mono.android.TypeManager.Activate ("App3.utils.LockWindowAccessibilityService, App3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onKeyEvent (android.view.KeyEvent p0)
	{
		return n_onKeyEvent (p0);
	}

	private native boolean n_onKeyEvent (android.view.KeyEvent p0);


	public void onAccessibilityEvent (android.view.accessibility.AccessibilityEvent p0)
	{
		n_onAccessibilityEvent (p0);
	}

	private native void n_onAccessibilityEvent (android.view.accessibility.AccessibilityEvent p0);


	public void onInterrupt ()
	{
		n_onInterrupt ();
	}

	private native void n_onInterrupt ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
