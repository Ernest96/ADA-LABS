#!/usr/bin/env node

var fibonacciCalculator = require('./fibonacciCalculator');
var amqp = require('amqplib/callback_api');
var fileStream = require('fs');

const queueName = 'ADA-LAB2';
const output = "D:\\Programming\\master\\ADA\\ADA-LABS\\ADA-LAB2\\output.txt";

var consumeMessage = async function(msg) {
    var number = msg.content.toString();
    console.log("[x] Received " + number);
    var result = await fibonacciCalculator.sleepyFibonacci(number);
    fileStream.appendFile(output, result + "\n", function (err) {
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

        channel.assertQueue(queueName, { durable: false });

        console.log("[*] Waiting for messages in %s. To exit press CTRL+C", queueName);

        channel.consume(queueName, consumeMessage, { noAck: true });
    });
});