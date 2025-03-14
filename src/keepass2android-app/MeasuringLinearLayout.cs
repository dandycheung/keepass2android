using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace keepass2android
{
	public class MeasuringLinearLayout : LinearLayout
	{
		protected MeasuringLinearLayout(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public MeasuringLinearLayout(Context context)
			: base(context)
		{
		}

		public MeasuringLinearLayout(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
		}

		public MeasuringLinearLayout(Context context, IAttributeSet attrs, int defStyleAttr)
			: base(context, attrs, defStyleAttr)
		{
		}

		public MeasuringLinearLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
			: base(context, attrs, defStyleAttr, defStyleRes)
		{
		}


		public class MeasureArgs
		{
			public int ActualHeight;
			public int ProposedHeight;

		}

		public event EventHandler<MeasureArgs> MeasureEvent;

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			MeasureArgs args = new MeasureArgs();

			args.ProposedHeight = MeasureSpec.GetSize(heightMeasureSpec);
			args.ActualHeight = Height;


			OnMeasureEvent(args);
			base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
		}

		protected virtual void OnMeasureEvent(MeasureArgs args)
		{
			var handler = MeasureEvent;
			if (handler != null) handler(this, args);
		}
	}
}