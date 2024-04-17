def takeInputs():

    while True:

        try:

            cost = float(input("Enter Initial vehicle cost: $"))

        except ValueError:

            print("invalid cost> ")

            continue

        break

    if cost == 0:

        print("\nThank you for using the depreciation schedule!")

        exit()

    while True:

        try:

            years = int(input("Enter number of years for the schedule: "))

        except ValueError:

            print("invalid number of years> ")

            continue

        break

    while True:

        try:

            type = input("Enter vehicle type sedan or SUV? S - Sedan V - SUV: ").lower()

            if type not in "sv":

                raise ValueError()

        except ValueError:

            print("Invalid vehicle type: ")

            continue

        break

 

    return cost, years, type

 

 

def depSchedule(cost, years, type):

    if type == "s":

        rate = 10 * 0.01

    else:

        rate = 11 * 0.01

    dep_amount = []

    current_value = []

    total_dep = 0

    for x in range(years):

        dep = round(cost * rate, 2)

        cur_value = cost - dep

        dep_amount.append(dep)

        current_value.append(cur_value)

        total_dep += dep

        cost -= dep

 

    return dep_amount, current_value, total_dep

 

def main():

    while True:

        cost, years, type = takeInputs()

        dep_amount, current_value, total_dep = depSchedule(cost, years, type)

 

        print(f"\n Year\t Depreciation Amt \t Current value of vehicle")

        year = 1

        for dep, cur_value in zip(dep_amount, current_value):

            print("\t{0:2d}\t\t {1:10.2f}\t\t\t{2:10.2f}".format(year, dep, cur_value))

            year += 1

 

        if type == "s":

            rate = 0.10

        else:

            rate = 0.11

        print("Vehicle depreciation rate: {0:3.2f}".format(rate))

        print("Total Depreciation at the end of the schedule: ${0:10.2f}\n".format(total_dep))

 

main()