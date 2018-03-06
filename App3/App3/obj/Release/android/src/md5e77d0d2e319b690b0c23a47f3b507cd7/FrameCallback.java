package md5e77d0d2e319b690b0c23a47f3b507cd7;


public class FrameCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.Choreographer.FrameCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_doFrame:(J)V:GetDoFrame_JHandler:Android.Views.Choreographer/IFrameCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("C1.Android.Core.FrameCallback, C1.Android.Core, Version=2.5.20173.241, Culture=neutral, PublicKeyToken=null", FrameCallback.class, __md_methods);
	}


	public FrameCallback ()
	{
		super ();
		if (getClass () == FrameCallback.class)
			mono.android.TypeManager.Activate ("C1.Android.Core.FrameCallback, C1.Android.Core, Version=2.5.20173.241, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void doFrame (long p0)
	{
		n_doFrame (p0);
	}

	private native void n_doFrame (long p0);

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
