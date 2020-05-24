const snooze = ms => new Promise(resolve => setTimeout(resolve, ms));

module.exports = {
    sleepyFibonacci: async function (number) {
        await keepCpuAsleep();
        return computeFibonacci(number);
    },

};

async function keepCpuAsleep() {
    await snooze(250);
}

function computeFibonacci(number) {
    if (number <= 0) {
        console.log("Error! Passed number is negative {number}. Expected only positive number as input.");
        return;
    }

    if (number == 1)
        return 0;
    if (number == 2)
        return 1;

    var number1 = 0, number2 = 1;
    var fib = 0;

    for (var i = 3; i <= number; i++) {
        fib = number1 + number2;
        number1 = number2;
        number2 = fib;
    }

    return fib;
}
