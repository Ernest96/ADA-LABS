#!/usr/bin/env node

var fibonacciCalculator = require('./fibonacciCalculator');
var amqp = require('amqplib/callback_api');
var fileStream = require('fs');
const QUEUE = 'ADA-LAB2';
const OUTPUT = "D:\\Programming\\master\\ADA\\ADA-LABS\\ADA-LAB2\\output.txt";

var consumeMessage = async function(msg) {
    var number = msg.content.toString();
    console.log("[x] Received " + number);
    var result = await fibonacciCalculator.sleepyFibonacci(number);
    fileStream.appendFile(OUTPUT, result + "\n", function (err) {
        if (err) throw err;
      });
}

amqp.connect('amqp://localhost', function(connectError, connection) {
    if (connectError) {
        throw connectError;
    }

    connection.createChannel(function(channelError, channel) {
        if (channelError) {
            throw channelError;
        }

        channel.assertQueue(QUEUE, { durable: false });

        console.log("[*] Waiting for messages in %s. To exit press CTRL+C", QUEUE);

        channel.consume(QUEUE, consumeMessage, { noAck: true });
    });
});