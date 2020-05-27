#!/usr/bin/env node

var fibonacciCalculator = require('./fibonacciCalculator');
var amqp = require('amqplib/callback_api');
var fileStream = require('fs');

const queueName = 'ADA-LAB2';
const output = "D:\\Programming\\master\\ADA\\ADA-LABS\\ADA-LAB2\\output.txt";

var queue_channel;

var consumeMessage = async function(msg) {
	try {
		var number = msg.content.toString();
		console.log("[x] Received " + number);
		
		var result = await fibonacciCalculator.sleepyFibonacci(number);
		
		fileStream.appendFile(output, result + "\n", function (err) {
			if (err) throw err;
			});
			
		queue_channel.ack(msg);
	}
	catch(ex) {
		console.log(ex);
	}
}


amqp.connect('amqp://localhost', function(connectError, connection) {
    if (connectError) {
        throw connectError;
    }

    connection.createChannel(function(channelError, channel) {
        if (channelError) {
            throw channelError;
        }
		
		queue_channel = channel;
		
		channel.prefetch(1);
        channel.assertQueue(queueName, { durable: false });

        console.log("[*] Waiting for messages in %s. To exit press CTRL+C", queueName);

        channel.consume(queueName, consumeMessage, { noAck: false });
    });
});