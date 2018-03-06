package md5c363810a8e8757aafd4082b73e07e8ef;


public class AnnotationLayer
	extends android.widget.FrameLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("C1.Android.Chart.Annotation.AnnotationLayer, C1.Android.Chart, Version=2.5.20173.241, Culture=neutral, PublicKeyToken=null", AnnotationLayer.class, __md_methods);
	}


	public AnnotationLayer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == AnnotationLayer.class)
			mono.android.TypeManager.Activate ("C1.Android.Chart.Annotation.AnnotationLayer, C1.Android.Chart, Version=2.5.20173.241, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public AnnotationLayer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == AnnotationLayer.class)
			mono.android.TypeManager.Activate ("C1.Android.Chart.Annotation.AnnotationLayer, C1.Android.Chart, Version=2.5.20173.241, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public AnnotationLayer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == AnnotationLayer.class)
			mono.android.TypeManager.Activate ("C1.Android.Chart.Annotation.AnnotationLayer, C1.Android.Chart, Version=2.5.20173.241, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}

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
