///saveScreenshot()
var fileString, dateTime;
dateTime = date_current_datetime();
fileString = string(date_get_month(dateTime)) + "-" + 
string(date_get_day(dateTime)) + "-" + 
string(date_get_year(dateTime)) + " " + 
string(date_get_hour(dateTime)) + "h" + 
string(date_get_minute(dateTime)) + "m" + 
string(date_get_second(dateTime)) + "s " + 
string(current_time) + "rtms";

screen_save(working_directory + "Scrnshots\" + fileString + ".png");
show_debug_message("Screenshot saved to " + working_directory + "Scrnshots\" + fileString + ".png");
