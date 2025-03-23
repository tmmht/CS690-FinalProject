namespace BusShuttle;

using Spectre.Console;


public class ConsoleUI {
    DataManager dataManager;

    public ConsoleUI() {
        dataManager = new DataManager();
    }

    public void Show() {
        
        var mode = AnsiConsole.Prompt(
				            new SelectionPrompt<string>()
				                .Title("Please select mode")
				                .AddChoices(new[] {
				                    "driver","manager"
				                }));


        if(mode=="driver") {

            var selectedDriver = AnsiConsole.Prompt(
				            new SelectionPrompt<Driver>()
				                .Title("Select a driver")
				                .AddChoices(dataManager.Drivers));
            Console.WriteLine("Now you are driving as "+selectedDriver.Name);


            Loop selectedLoop = AnsiConsole.Prompt(
				            new SelectionPrompt<Loop>()
				                .Title("Select a loop")
				                .AddChoices(dataManager.Loops));
            Console.WriteLine("You selected "+selectedLoop.Name+" loop!");

            string command;

            do {
                Stop selectedStop = AnsiConsole.Prompt(
				            new SelectionPrompt<Stop>()
				                .Title("Select a stop")
				                .AddChoices(selectedLoop.Stops));
                Console.WriteLine("You selected "+selectedStop.Name+" stop!");

                int boarded = AnsiConsole.Prompt(new TextPrompt<int>("Enter number of boarded passengers:"));

                PassengerData data = new PassengerData(boarded, selectedStop, selectedLoop, selectedDriver);

                dataManager.AddNewPassengerData(data);

                command = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("What's next?")
				                        .AddChoices(new[] {
				                            "continue","end"
				                        }));


            } while(command!="end");

        } else if(mode=="manager") {

            string command;
            do {
                
                command = AnsiConsole.Prompt(
				                    new SelectionPrompt<string>()
				                        .Title("What do you want to do?")
				                        .AddChoices(new[] {
				                            "show busiest stop","add stop","delete stop", "list stops", "end"
				                        }));

                if(command=="add stop") {
                    var newStopName = AnsiConsole.Prompt(new TextPrompt<string>("Enter new stop name:"));
                    dataManager.AddStop(new Stop(newStopName));
                } else if(command=="delete stop") {
                    Stop selectedStop = AnsiConsole.Prompt(
				            new SelectionPrompt<Stop>()
				                .Title("Select a stop")
				                .AddChoices(dataManager.Stops));
                    dataManager.RemoveStop(selectedStop);
                } else if(command=="list stops") {
                    var table = new Table();

                    table.AddColumn("Stop Name");

                    foreach(var stop in dataManager.Stops) {
                        table.AddRow(stop.Name);
                    }
                    AnsiConsole.Write(table);

                } else if(command=="show busiest stop") {
                    var result = Reporter.FindBusiestStop(dataManager.PassengerData);
                    Console.WriteLine("The busiest stop is: "+result.Name);
                }


            } while(command!="end");

        }
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}