   M S B A M L   `   `   `  ����b  ^Capgemini.PolicyEndorsement.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:Capgemini.PolicyEndorsement.Application'Capgemini.PolicyEndorsement.Application  5   	   9    3Capgemini.PolicyEndorsement.Application.AddCustomer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Glocal5clr-namespace:Capgemini.PolicyEndorsement.Application     5   	     � Title$  AddCustomer��5   	   $��502.444��6   $��480.263��6.   .��5	      �� -   5        .@�5
   
   �� $��Add Customer��6   .��5      � 
�������$��0,0,2,-5q�6)    � ColumnDefinitions 5      �� 5      $
��149* �6'   �� 5      $
��315* �6'   .+�5      �� $��Customer Name :��6   $
��Left=�62   $��	30,30,0,0q�6M   $	��Top=�6`   $
��Bold"�6x   �� 5      $��Address       :��6   $
��Left=�62   $��	30,84,0,0q�6M   $	��Top=�6`   $
��Bold"�6x   �� 5      $��Telephone     :��6   $
��Left=�62   $��
30,134,0,0q�6M   $	��Top=�6a   $
��Bold"�6y   �� 5      $��Gender        :��6   $
��Left=�62   $��
30,187,0,0q�6M   $	��Top=�6a   $
��Bold"�6y   �� 5      $��Date of Birth :��6   $
��Left=�62   $��
30,242,0,0q�6M   $	��Top=�6a   $
��Bold"�6y   �� 5      $��Smoker        :��6   $
��Left=�62   $��
30,303,0,0q�6M   $	��Top=�6a   $
��Bold"�6y   �� 5      $��Hobbies       :��6   $
��Left=�62   $��
30,362,0,0q�6M   $	��Top=�6a   $
��Bold"�6y   �� 5      -   5        $��txtCustomerName��5      $
��Left=�61   $��23��6L   $��21.361,34,0,0q�6X    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $	��120��6�   $��1��6�   �� 5      -   5        $��
txtAddress��5      $
��Left=�6,   $��23��6G   $��21.361,84,0,0q�6S   $
 Wrap=�6j   $	��Top=�6   $	��120��6�   $��1��6�   �� 5      -   5        $��txtTelephone��5      $
��Left=�6.   $��23��6I   $��21.361,134,0,0q�6U   $
 Wrap=�6m   $	��Top=�6�   $	��120��6�   $��1��6�   	� 5      -   5        $��	rdGenderM��5       	� 	GroupName$ rdGender��6/   $
��Male��6D   $
��Left=�6S   $��21.361,193,0,0q�6n   $	��Top=�6�   $��1��6�   	� 5      -   5        $��	rdGenderF��5      $ rdGender��6/   $��Female��6D   $
��Left=�6U   $��94.361,193,0,0q�6p   $	��Top=�6�   $��1��6�   	� 5      -   5        $��	rdGenderO��5      $ rdGender��60   $��Others��6E   $
��Left=�6V   $��173.361,193,0,0q�6q   $	��Top=�6�   $��1��6�   	� 5      -   5        $��	rdSmokerY��5      $ rdSmoker��6/   $	��Yes��6D   $
��Left=�6R   $��21.361,303,0,0q�6m   $	��Top=�6�   $��1��6�   	� 5      -	   5        $��	rdSmokerN��5      $ rdSmoker��6/   $��No��6D   $
��Left=�6Q   $��94.361,303,0,0q�6l   $	��Top=�6�   $��1��6�   �� 5      -
   5        $��
txtHobbies��5      $
��Left=�6,   $��23��6G   $��21.361,362,0,0q�6S   $
 Wrap=�6k   $	��Top=�6   $	��120��6�   $��1��6�   (  "System.Windows.Controls.DatePicker  5       -   5        $��dpDob��5       $
��Left=�6*   $��21.361,242,0,0q�6E   $	��Top=�6]   $	��120��6u   $��1��6�   �� 5!      -   5        $��	btnSubmit��5!      $��Submit��6*   $
��Left=�6;   $��108,407,0,0q�6V   $	��Top=�6k   $��75��6�   $��2��6�   �� 5"      -   5        $��btnClear��5"      $��Clear��6)   $
��Left=�69   $��94.361,407,0,0q�6T   $	��Top=�6l   $��75��6�   $��1��6�   5#      5$      �� 5%   
   $��View Customer Info ��6   
�������.��5'      &   System.Windows.Controls.DataGrid  -   5        $��
dgCustomer��5'      $��
10,33,6,68q�6)   5)      5*      5+      5,      