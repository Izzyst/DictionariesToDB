A set of helper classes for the basic MVVM app. TrakPropertyChanged implements the INotifyPropertyChanged interface to save 
coding. TrakViewModel inherits TrakPropertyChanged and has 2 properties, VMList and EditRecord.

TrakPropertyChanged
   Implements the INotifyPropertyChanged interface.
   
   Method:
      RaisePropertyChanged(); - Add this method to all properties when you set the value for the property.

   Example:
      using ComponentTrak.MVVM;

      class MyViewModel : TrakPropertyChanged
	  { 
	     private string _Name = "";
		 public string Name { get { return _Name; } set { _Name = value; RaisePropertyChanged(); } }
	  }	  

TrakViewModel
   Implements the INotifyPropertyChanged interface and has properties for the basic MVVM using C# generics.

   Method:
      RaisePropertyChanged(); - Add this method to all properties when you set the value for the property.

   Properties:
      VMList - Declares an observable collection using T.
	  EditRecord - Declares an edit record.

   Example:
      Always add the using statement at the top of your cs file.

         using ComponentTrak.MVVM;

	  This class also implements the TrakPRopertyChanged class so you can use the RaisePropertyChanged 
	  on any of your custom properties.

         class MyViewModel : TrakViewModel
         {
	        private string _Name = "";
		    public string Name { get { return _Name; } set { _Name = value; RaisePropertyChanged(); } }
         }

      In your MyContentPage.xaml to use the built-in list of models, you could bind it to your ListView
	     
		 <ListView ItemsSource="{Binding VMList}"/>
	     
      In your MyContentPage.xaml to edit a record using the built-in model, you could bind your grid or any 
	  control to the EditVMRecord that has a model with multiple properties. This example shows if the model 
	  was a customer model with a couple of properties, Name and Address

	     <Grid BindingContext="{Binding EDitRecord}">
		    <Entry Text="{Binding Path=Name, Mode=TwoWay}"/>
		    <Entry Text="{Binding Path=Address, Mode=TwoWay}"/>
		 </Grid>


TrakEditViewModel
   Implements the INotifyPropertyChanged interface and has properties for the basic MVVM using C# generics.

   Method:
      RaisePropertyChanged(); - Add this method to all properties when you set the value for the property.

   Properties:
      VMList - Declares an observable collection using T.
	  EditRecord - Declares an edit record.

   Example:
      Always add the using statement at the top of your cs file.

         using ComponentTrak.MVVM;

	  This class also implements the TrakPRopertyChanged class so you can use the RaisePropertyChanged 
	  on any of your custom properties.

         class MyViewModel : TrakViewModel
         {
	        private string _Name = "";
		    public string Name { get { return _Name; } set { _Name = value; RaisePropertyChanged(); } }

			private int _Age = 0;
			public int Age { get { return _Age; } set { _Age = value; RaisePropertyChanged(); } }
         }

      In your MyContentPage.xaml to use the built-in list of models, you could bind it to your ListView
	     
		 <ListView ItemsSource="{Binding VMList}"/>
	     
      In your MyContentPage.xaml to edit a record using the built-in model, you could bind your grid or any 
	  control to the EditRecord that has a model with multiple properties. This example shows if the model 
	  was a customer model with a couple of properties, Name and Address

	     <Grid BindingContext="{Binding EDitRecord}">
		    <Entry Text="{Binding Path=Name, Mode=TwoWay}"/>
		    <Entry Text="{Binding Path=Address, Mode=TwoWay}"/>

			<Button Text="Save" IsEnabled="{Binding Path=IsModelValid}"/>
		 </Grid>
      
	  In this part of the example, we'll add validation name to be required and string length.

	     var vm = BindingContext as TrakEditViewModel<Customer>;
         vm.ValidateRequired("Name", "Name is required");
		 vm.ValidateStringLength("Name", 3, 100, "Name must be 3 to 100 characters");
         vm.ValidateStringLength("Name", 4, 100, "Address must be 4 to 100 characters");
		 vm.ValidateRangeLimit("Age", 20, 99, "Age must be 20 to 99");

	 



