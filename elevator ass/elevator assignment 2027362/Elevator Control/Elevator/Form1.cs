using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Elevator  
{
   
    public partial class ElevatorControl : Form
    {        bool go_down = false;
             bool go_up = false;
             bool arrived_G = false;
             bool arrived_1 = false;
             Door da = new Door();
             Lift la = new Lift();
             int increment = 0;
             DatabaseCommand dc = new DatabaseCommand();
        
            

           

        public ElevatorControl()
        {
            InitializeComponent();  
           
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            btn_down.BackColor = Color.Blue;
                 go_down = true;

                timer_door_close_up.Enabled = true; // timer to close door of first floor
                timer_door_open_up.Enabled = false; // timer to open door of first floor
                 arrived_1 = false;

                 btn_up.Enabled = false;
                 btn_1.Enabled = false;
                 btn_emergency.Enabled = false;
            
           
        }

       

        private void timer_door_close_up_Tick(object sender, EventArgs e) // timer to close the door of first floor
        {
            if (door_left_up.Left <=110 && door_right_up.Left >=162)
            {
                da.door_close(door_left_up, door_right_up, door_left_down, door_right_down);//call the method to cose the door of the first floor
               
            }
            else
            {
                automatic_door_close.Enabled = false; // timer to close door after 5 sec
                increment = 0;

                timer_door_close_up.Enabled = false; //timer to close door of first floor
                
                
                dc.insertdata("First Floor Door Closing"); //data inserted in database
                LoadData(); // shows databse reccord in GUI
                btn_close.BackColor = Color.White;
                
                
               if(go_down==true){
                   automatic_door_close.Enabled = false;
                   increment = 0;

                   display_panel.Image = global::Elevator.Properties.Resources.down;
                   picture_up.Image = global::Elevator.Properties.Resources.down;
                   picture_down.Image = global::Elevator.Properties.Resources.down;
                   timer_lift_down.Enabled = true; //timer to move the elavator to ground floor
                   go_down = false;
                   btn_close.BackColor = Color.White;

               }

            }

      }

        private void timer_lift_down_Tick(object sender, EventArgs e) //timer to move lift to do
        {
            if (picture_lift.Top <= 352)
            {
                la.lift_down(picture_lift); // calls the method to move the elevator
                btn_emergency.Enabled = false;
               
               
           }
            else
            {
                
                
                timer_lift_down.Enabled = false;
                dc.insertdata("Lift going down"); // data inserted in databse
                LoadData(); // shows database record in GUI 
                btn_1.Enabled = false;
                btn_G.Enabled = true;
                btn_up.Enabled = false;
               
                
                 timer_door_open_down.Enabled = true; //timer to open door of ground floor
               
                timer_door_close_down.Enabled = false;// timer to close door of ground floor
                arrived_G = true; //arrival of elevator in ground floor
           
             
               
                display_panel.Image = global::Elevator.Properties.Resources.G;

                    picture_up.Image = global::Elevator.Properties.Resources.G;
                picture_down.Image = global::Elevator.Properties.Resources.G;
                


            }
        }

        private void timer_door_open_down_Tick(object sender, EventArgs e)// timer to open the door of ground floor
        {
            increment = 0;
            if (door_left_down.Left >= 61 && door_right_down.Left <= 208)
            {
                da.door_open(door_left_down, door_right_down,door_right_up, door_left_up);
                btn_1.Enabled = true;
                btn_up.Enabled = true;
            }
            else
            {
                automatic_door_close.Enabled = true;
                timer_door_open_down.Enabled = false;
                dc.insertdata("Ground Floor Door Opening");// data inserted in database
                LoadData();// shows database record in GUI

                btn_up.Enabled = true;
                btn_down.Enabled = true;
                btn_G.Enabled = true;
                btn_emergency.Enabled = true;
                btn_down.BackColor = Color.White;
                btn_G.BackColor = Color.White;
                btn_open.BackColor = Color.White;
                btn_close.BackColor = Color.White;
              

                automatic_door_close.Enabled = true;
            }

        }



        private void btn_up_Click(object sender, EventArgs e) /*opening door button of 
                                                                first floor*/
        {
            btn_up.BackColor = Color.Blue;
                go_up = true;
                timer_door_close_down.Enabled = true; //timer to close door of ground floor
                timer_door_open_down.Enabled = false; // timer to open door of ground floor
                arrived_G = false;

                btn_down.Enabled = false ;
                btn_G.Enabled = false;
                btn_emergency.Enabled = false;

        }

        private void timer_door_close_down_Tick(object sender, EventArgs e)
        {

            if (door_left_down.Left <= 110 && door_right_down.Left >= 162)
            {
                da.door_close(door_left_up, door_right_up, door_left_down, door_right_down);
                 /* calls the  methods (door_close) of class Door to close the door
                 through the object (da).
                */ 
            }

            else {
                increment = 0;
                automatic_door_close.Enabled = false;
                     timer_door_close_down.Enabled = false;
                     dc.insertdata("Ground Floor Door Closing"); //database record
                     LoadData();// show the record in GUI
                     btn_close.BackColor = Color.White;
       
               if(go_up==true){
                    display_panel.Image = global::Elevator.Properties.Resources.up;
                    picture_up.Image = global::Elevator.Properties.Resources.up;
                    picture_down.Image = global::Elevator.Properties.Resources.up;
                    timer_lift_up.Enabled = true;/* timer set to move the evelator to the
                                                      first floor*/
                    go_up = false;
                    
                }
             }
        }

        private void timer_lift_up_Tick(object sender, EventArgs e)
        {
            if (picture_lift.Top >= 51)
            {
                la.lift_up(picture_lift);/* calls the method(lift_up) from the class
                                      Lift to move the elevator through the object (la)*/
                btn_G.Enabled = false;
                btn_emergency.Enabled = false;
                btn_down.Enabled = false;
                
                
            }
            else 
            {
                timer_lift_up.Enabled = false; //timer to move the elavator to first floor
                dc.insertdata("Lift Going Up");// database record
                LoadData();// shows record to GUI
                timer_door_open_up.Enabled = true;//timer to open door
                timer_door_close_up.Enabled = false;//timer to close door
                arrived_1 = true;
               
               

                display_panel.Image = global::Elevator.Properties.Resources._1;
                picture_up.Image = global::Elevator.Properties.Resources._1;
                picture_down.Image = global::Elevator.Properties.Resources._1;
              
            }
        }

        private void timer_door_open_up_Tick(object sender, EventArgs e)
        {
            if (door_left_up.Left >= 61 && door_right_up.Left <= 208)
            {
               da.door_open(door_left_up, door_right_up, door_left_down, door_right_down);
                /* calls the  methods (door_open) of class Door to open the door
                 through the object (da).
                */        
            }
            else
            {
                automatic_door_close.Enabled = true;
                timer_door_open_up.Enabled = false; //timer to open door
                dc.insertdata("First Floor Door Opening"); //database record
                LoadData(); //shows data to GUI
                btn_down.Enabled = true;
                btn_G.Enabled = true;
                btn_1.Enabled = true;
                btn_emergency.Enabled = true;
                automatic_door_close.Enabled = true;
                btn_open.BackColor = Color.White;
                btn_1.BackColor = Color.White;
                btn_up.BackColor = Color.White;
                btn_close.BackColor = Color.White;
           }
        }

     

        private void btn_G_Click(object sender, EventArgs e)
        {
            btn_G.BackColor = Color.Blue;
            btn_1.Enabled = false;
            arrived_1 = false;
            go_down = true;
            timer_door_close_up.Enabled = true;
            timer_door_open_up.Enabled = false;
            btn_up.Enabled = false;

        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            btn_1.BackColor = Color.Blue;
            
            go_up = true;
            arrived_G = false;
            timer_door_close_down.Enabled =true;
            timer_door_open_down.Enabled = false;
            btn_down.Enabled = false;
            btn_G.Enabled = false;
        }

       
        private void btn_close_Click(object sender, EventArgs e)
        {
            btn_close.BackColor = Color.Blue;
            if (arrived_G == true)
            {
               
                timer_door_close_down.Enabled = true;
                timer_door_open_down.Enabled = false;
            }
            else if (arrived_1 == true)
            {
                
                timer_door_close_up.Enabled = true;
                timer_door_open_up.Enabled = false;
            }

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            btn_open.BackColor = Color.Blue;
              if (arrived_G==true)
            {
               
                timer_door_open_down.Enabled = true;
                timer_door_close_down.Enabled = false;
            }
            else if (arrived_1==true)
            {

                timer_door_close_up.Enabled = false;
                timer_door_open_up.Enabled = true;
            }

        }

        private void btn_emergency_Click(object sender, EventArgs e)
        {
            dc.insertdata("Emergency Exit");
            LoadData();
            display_panel.Image = global::Elevator.Properties.Resources.alarmbellbutton;
            timer_lift_down.Enabled = false;
            timer_lift_up.Enabled = false;
            timer_door_open_down.Enabled = true;
            timer_door_open_up.Enabled = true;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void door_left_up_Click(object sender, EventArgs e)
        {

        }

        private void ElevatorControl_Load(object sender, EventArgs e)
        {
            GlobalConnection.DbConnection(); //enables database connection
            
        }
        public void LoadData()
        {
            try
            {
                DatabaseCommand dc = new DatabaseCommand();
                DataTable dt = dc.ViewData();
                dataGridView1.DataSource = dt; //import data in datagrid view
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Elevator Control");
            }

        }

        private void clrLog_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null; // clears the data of datagrid view
            dc.remove();                    // delete record of database
           
            
           

        }

        private void picture_down_Click(object sender, EventArgs e)
        {

        }
        private void automatic_door_close_Tick(object sender, EventArgs e) //timer to close door automatically
        {
            increment++;
            if (increment>=240 && arrived_G==true)
            {

                timer_door_close_down.Enabled = true;
                
               
            }
            else if (increment >= 240 && arrived_1 == true)
            {

                timer_door_close_up.Enabled = true;
                

            }
        }

       
            
           
            
        }
    }

 