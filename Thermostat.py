"""
Travis Womack
Thermostat Simulator
Game 2341 - Spring 2017

Pseudo Code - revised

Thermostat Simulator
Declare a number for the minimum desired temperature heatOn, initialize to 66
Declare a number for the maximum desired temperature coolOn, initialize to 78
Declare a variable called opType that has the following values:
System Off - off, no heating or cooling
Heat On - for heating
Cool On - for cooling
Auto - for keeping temperature within a desired range
Initialize to 0 - system off
Declare a string variable to store the operational type as defined above
Declare and define a function that prints out the operational modes

Declare a variable called sysStatus that has the following values
System off
Heating on
Cooling on
System idle
Initialize to 0 system idle

Declare a number for the ambient temperature temp, initialize to 72

Declare and define a function test that
Gets ambient temp from user to test Thermostat application if thermometer not detected

Declare and define a function getTemp that
Detects thermometer sets to test mode if not found
Gets ambient temperature from hardware thermometer or user if in test mode
Returns the ambient temperature

Activate Thermostat
While Thermostat active
    Get temperature set points from user or accept current
        Get heatOn temperature from user
        Get coolOn temperature from user
        While heatOn >= coolOn ( error check )
            Get heatOn temperature from user
            Get coolOn temperature from user
            If heatOn >= coolOn then
                Error: condition heatOn < coolOn must exist
    Get operational mode from user or accept current
        Get operational mode of 0, 1, 2, or 3
    do 10 tests of the simulator with current settings
        temp = getTemp temp
        Print temp

        If ( opType == 0 )
            Set sysStatus = 0 // system off

        else If(opType == 1)
            Set sysStatus = 3 // system idle
            If(temp < heatOn )
                Turn on heating unit
                Set sysStatus = 1 // heating on
            else:  # If(temp > heatOn )
                Turn off heating unit

        else If(opType == 2)
            Set sysStatus = 3 // system idle
            if temp < coolOn
                Turn off cooling unit
            else:
                Turn on cooling unit
                Set sysStatus = 2 // cooling on

        else:
            if intTemp < intHeatOn or intTemp > intCoolOn:
                Set sysStatus = 3 // system idle
            if If(temp < heatOn )
                Turn on heating unit
                Set sysStatus = 1 // heating on
            else:
                Turn off cooling unit
                Set sysStatus = 3 // system idle
            else:
                Turn off heating unit
                Turn off cooling unit
                Set sysStatus = 3 // system idle

        Print system status
        Print system operational mode
        Keep thermostat active?

Deactivate Thermostat
Set sysSatus = 0 //system off
Set opType = 0 //system off
Disconnect from thermometer, heating and cooling units
Print system status
Print system operational mode
Print connection status to thermometer
print thermostat active status

*********************End Pseudo code

Travis Womack
Thermostat Simulator
Game 2341 - Spring 2017
Output

Activate Therostat? y/n y

Reset current temperature set points of 78 and 66 ? y/n n
Reset current operational mode of Off ? y/n y
Operational modes allowed:
0 - off, no heating or cooling - turned off
1 - for heating
2 - for cooling
3 - for keeping temperature within a desired range
Enter operational mode: 0, 1, 2, 3: 3

Current ambient temperature: 61
If Cool ON, Turn OFF the cooling unit
Turn ON the heating unit
Current system status: Heat On
Current operations mode: Auto


Current ambient temperature: 59
If Cool ON, Turn OFF the cooling unit
Turn ON the heating unit
Current system status: Heat On
Current operations mode: Auto


Current ambient temperature: 88
If Heat ON, Turn OFF the heating unit
Turn ON the cooling unit
Current system status: Cool On
Current operations mode: Auto


Current ambient temperature: 67
If Heat ON, Turn OFF the heating unit
If Cool ON, Turn OFF the cooling unit
Current system status: System Idle
Current operations mode: Auto


Current ambient temperature: 58
If Cool ON, Turn OFF the cooling unit
Turn ON the heating unit
Current system status: Heat On
Current operations mode: Auto


Current ambient temperature: 95
If Heat ON, Turn OFF the heating unit
Turn ON the cooling unit
Current system status: Cool On
Current operations mode: Auto


Current ambient temperature: 59
If Cool ON, Turn OFF the cooling unit
Turn ON the heating unit
Current system status: Heat On
Current operations mode: Auto


Current ambient temperature: 71
If Heat ON, Turn OFF the heating unit
If Cool ON, Turn OFF the cooling unit
Current system status: System Idle
Current operations mode: Auto


Current ambient temperature: 73
If Heat ON, Turn OFF the heating unit
If Cool ON, Turn OFF the cooling unit
Current system status: System Idle
Current operations mode: Auto


Current ambient temperature: 53
If Cool ON, Turn OFF the cooling unit
Turn ON the heating unit
Current system status: Heat On
Current operations mode: Auto

Keep Thermostat active? y/n
Current system status: System Idle
Current operations mode: Off
Thermostat connected to thermometer: No
Thermostat active? No

Process finished with exit code
"""

# Thermostat program code
import random

intHeatOn = 66  # Declare a number for the minimum desired temperature heatOn, initialize to 66
intCoolOn = 78  # Declare a number for the maximum desired temperature coolOn, initialize to 78
tupOpType = ("Off", "Heat", "Cool", "Auto")  # Declare a variable called opType that has the following values:
# 	0 - off, no heating or cooling - turned off
# 	1 - for heating
# 	2 - for cooling
# 	3 - for keeping temperature within a desired range
intOpType = 0
strOpMode = tupOpType[0]  # Initialize to 0 - system off
tupSysStatus = (
    "System Off", "Heat On", "Cool On",
    "System Idle")  # Declare a variable called sysStatus that has the following values
# 	0 - system off
# 	1 - heating on
# 	2 - cooling on
# 	3 - system idle
strStatus = tupSysStatus[3]  # Initialize to 0 system idle
intTemp = 72  # Declare a number for the ambient temperature temp, initialize to 72
intThermometer = 72  # Declare a number for the connected thermomenter temperature intThermometer, initialize to 72

def test():  # Declare and define a function test that
    return int(input(
        "Enter current temperature: "))  # Gets ambient temp from user to test Thermostat application if thermometer not detected

bolConnected = False  # Detects thermometer sets to test mode if not found False else True

def getTemp():  # Declare and define a function getTemp that
    if not bolConnected:  # Gets ambient temperature from hardware thermometer or user if in test mode
        intTmp = random.randint(45,100)
    else:
        intTmp = intThermometer
    return intTmp  # Returns the ambient temperature

def printOpTypes():
    print("Operational modes allowed:")
    print("0 - off, no heating or cooling - turned off")
    print("1 - for heating")
    print("2 - for cooling")
    print("3 - for keeping temperature within a desired range")

intOpType = 0 # default start system off
strAcitvate = input('Activate Therostat? y/n ')  # Activate Thermostat
bolActivate = (strAcitvate == "Y" or strAcitvate == "y")
print (" ")
while bolActivate:  # While Thermostat active
    # Get temperature set points from user
    strUsrResp = input("Reset current temperature set points of " + str(intCoolOn) + " and " + str(intHeatOn) + " ? y/n ")
    if strUsrResp == "y":
        intHeatOn = int(input("Enter Turn on Heating temperature: ")) # Get heatOn temperature from user
        intCoolOn = int(input("Enter Turn on Airconditioning temperature: "))  # Get coolOn temperature from user
        while intHeatOn >= intCoolOn:  # While heatOn >= coolOn
            intHeatOn = int(input("Enter Turn on Heating temperature: ")) # Get heatOn temperature from user
            intCoolOn = int(input("Enter Turn on Airconditioning temperature: "))  # Get coolOn temperature from user
            if intHeatOn >= intCoolOn:  # If heatOn >= coolOn then
                print(
                    "Error: the Heating temperatture greater than or equal to the Aircondidioning temperature.  Re-Enter")  # Error: condition heatOn < coolOn must exist
    # end if
    # Get operational mode from user
    if input("Reset current operational mode of " + tupOpType[intOpType] + " ? y/n ") == "y":
        intOpType = -1 # force get operational mode
        while not (intOpType == 0 or intOpType == 1 or intOpType == 2 or intOpType == 3):
            printOpTypes()
            intOpType = int(input("Enter operational mode: 0, 1, 2, 3: "))
    # end if
    for simNum in range ( 0, 10 ): # do 10 tests of the simulator with current settings
        strOpMode = tupOpType[intOpType]
        intTemp = getTemp()  # temp = getTemp temp
        print(" ")
        print("Current ambient temperature: " + str(intTemp))  # Print temp
        if intOpType == 0:  # If ( opType == 0 )
            strStatus = tupSysStatus[0]  # Set sysStatus = 0 // system off

        elif intOpType == 1:  # Else If(opType == 1)
            strStatus = tupSysStatus[3]  # Set sysStatus = 3 // system idle
            if intTemp < intHeatOn:  # If(temp < heatOn )
                print("Turn ON the heating unit")  # Turn on heating unit
                strStatus = tupSysStatus[1]  # Set sysStatus = 1 // heating on
            else:  # If(temp > heatOn )
                print("Turn OFF the heating unit")  # Turn off heating unit

        elif intOpType == 2:  # Else If(opType == 2)
            strStatus = tupSysStatus[3]  # Set sysStatus = 3 // system idle
            if intTemp < intCoolOn:  # If(temp < coolOn )
                print("Turn OFF the cooling unit")  # Turn off cooling unit
            else:  # If(temp > coolOn )
                print("Turn ON the cooling unit")  # Turn on cooling unit
                strStatus = tupSysStatus[2]  # Set sysStatus = 2 // cooling on

        else:
            if intTemp < intHeatOn or intTemp > intCoolOn:
                if intTemp < intHeatOn:  # If(temp < heatOn )
                    print("If Cool ON, Turn OFF the cooling unit")  # Turn off cooling unit
                    print("Turn ON the heating unit")  # Turn on heating unit
                    strStatus = tupSysStatus[1]  # Set sysStatus = 1 // heating on
                else:  # If(temp > heatOn )
                    print("If Heat ON, Turn OFF the heating unit")  # Turn off heating unit
                    print("Turn ON the cooling unit")  # Turn off cooling unit
                    strStatus = tupSysStatus[2]  # Set sysStatus = 3 // system idle
            else:
                print("If Heat ON, Turn OFF the heating unit")  # Turn off heating unit
                print("If Cool ON, Turn OFF the cooling unit")  # Turn off cooling unit
                strStatus = tupSysStatus[3]  # Set sysStatus = 3 // system idle

        print("Current system status: " + strStatus)  # Print system status
        print("Current operations mode: " + strOpMode)  # Print system operational mode
        print(" ")
    # end for loop
    strActivate = input('Keep Thermostat active? y/n ')  # Activate Thermostat
    bolActivate = (strActivate == "Y" or strActivate == "y" or strActivate == "")

# Deactivate Thermostat
strStatus = tupSysStatus[3]  # Set sysSatus = 0 //system off
strOpMode = tupOpType[0]  # Set opType = 0 //system off
bolActivate = False
print("Current system status: " + strStatus)  # Print system status
print("Current operations mode: " + strOpMode)  # Print system operational mode
strConnected = ""
if not bolConnected:
    strConnected = "No"
    
print("Thermostat connected to thermometer: " + strConnected)  # Print connection status to thermometer
strActivate = ""
if not bolActivate:
    strActivate = "No"
    
print("Thermostat active? " + strActivate)  # print thermostat active status
