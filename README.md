
# ADA-LABS
Aplicatii Distribuite Avansate

# Lab1
A fost implementata procesarea cu ajutorul TPL (OpenMP) si AKKA.NET (OpenMPI). Pe parcursul lucrarii s-a facut o paralela intre aceste 2 metodologii. Totodata a fost testata aplicatia cu Sleepy Fibonacci si Busy Fibonacci.

## TPL sleepy

![TPL](https://scontent.fkiv5-1.fna.fbcdn.net/v/t1.15752-9/100063783_1204216053243009_1698840568374755328_n.png?_nc_cat=107&_nc_sid=b96e70&_nc_eui2=AeFSNdRVgQ1tSOJ14CkEXa7XaGP6Tj_O81xoY_pOP87zXGC2Q3BEjcawTr4boLQaOJTSSG2k7fochPNfMCjPNWVF&_nc_ohc=GtK_rXWTo48AX_h6CP8&_nc_ht=scontent.fkiv5-1.fna&oh=de7c6144fb2da5d680e53a68fd3e7f02&oe=5EF7ACF5)

## TPL busy
![tpl busy](https://scontent.fkiv5-1.fna.fbcdn.net/v/t1.15752-9/99310854_1659509620869272_8812033866341548032_n.png?_nc_cat=104&_nc_sid=b96e70&_nc_eui2=AeGsAm4AP5zMjO_4hVFjFnJzPvDQAlTQ3pw-8NACVNDenEIv06yTF8XN5U8KXhqXQeYjyr5DtANELWFpmrkX4-Iz&_nc_ohc=bylriR5xu4IAX_ZdFQL&_nc_ht=scontent.fkiv5-1.fna&oh=f34de81a9c3ccfc40e466623c90ac596&oe=5EF7AD5C)

*Se observa ca atunci cind rulam Bussy Fibonacci cu mai multe fire de executie - timpul de executie creste mai rapid ca cel cind utilizam sleepy Fibonacci. Asta se datoreaza faptului ca busy fibonacci va consuma resursele procesorului mai drastic ca sleepy fibonacci. Sleepy fibonacci presupune adormirea firului de executie, astfel se reduce consumul.*

## AKKA.Net sleepy

![enter image description here](https://scontent.fkiv5-1.fna.fbcdn.net/v/t1.15752-9/99277309_270468117406110_1783144699016511488_n.png?_nc_cat=107&_nc_sid=b96e70&_nc_eui2=AeEpZTcw_MPbLhYK4QAXmE-MtMp6lQPbf9G0ynqVA9t_0YvIFmqe6sVFEFXZhC5PBJdMyRbP_w4i5kui9MP3LHvc&_nc_ohc=aJAymchK6swAX8LmQ4l&_nc_ht=scontent.fkiv5-1.fna&oh=b9b61973a484989d5db61282369cbc06&oe=5EF8F069)


## AKKA.Net busy
![enter image description here](https://scontent.fkiv5-1.fna.fbcdn.net/v/t1.15752-9/99130321_257054945625483_7082160647424704512_n.png?_nc_cat=105&_nc_sid=b96e70&_nc_eui2=AeG7oqhbYE7LyFjb4En8SjdmGnUpvtjJfZwadSm-2Ml9nB2g4KPKkYs-UoXyd8P13OzwfN9RmfgD-Udn8srs6eRz&_nc_ohc=dPfNI2yu1QwAX9rFiRJ&_nc_ht=scontent.fkiv5-1.fna&oh=27dbc7b3fbfedd09cc88d23b1ba6f34d&oe=5EF72415)

Concluzia aici intre sleepy si busy este aceeasi ca si in cazul cu TPL.

## TPL vs AKKA

![](https://i.imgur.com/YtXmtAK.png)

![](https://i.imgur.com/S3L5npq.png)



Daca sa facem o paralela intre aceste doua abordari: Timpul de executie in cazul cu TPL este un pic mai mic. Asta se datoreaza faptului ca nu este nevoie sa sincronizam actorii si sa transmitem mesaje la actori, insa totodata AKKA.NET permite o decuplare mai eficienta. Pe linga aceasta AKKA.NET permite incapsularea acestui proces de concurenta. Noi nu avem de afacere cu fire de executie, fiecare actor ruleaza pe firul sau de executie si este adormit atit timp cind nu primeste mesaje. Cu ajutorul AKKA.NET se poate foarte eficient de asigurat comunicare intre aplicatii aflate la distanta si prelucrarea mesajelor.

**In momentul cind numarul de fire de executie depaseste numarul de fire de executie a procesorului, timpul de executie creste.**

# LAB2

In aceasta lucrare s-a folosit RabbitMQ. O coada de mesaje ce poate fi folosita de mai multe procese/aplicatii. Cu ajutorul RabbitMQ putem asigura comunicarea intre mai multe procese independent de limbajul de programare in care aceste aplicatii (procese) au fost scrise. In acest laborator au fost folosite limbajele C# si JavaScript (Node JS).

![lab2](https://scontent.fkiv5-1.fna.fbcdn.net/v/t1.15752-9/100597991_1113288285701188_8732611133535420416_n.png?_nc_cat=101&_nc_sid=b96e70&_nc_eui2=AeF_PJcXg5LkUuIjD85r9bdc1JAiucGXKS_UkCK5wZcpLxcAWrhboC2RPf0tAjSf3xWq63kbI8PdatmkKw_aqlxu&_nc_ohc=Ox8P5y2Ami8AX_z_zKs&_nc_ht=scontent.fkiv5-1.fna&oh=d0c29952d79ea89cc5f74c528099a9c6&oe=5EF6017A)

Aici este reflectat cum un Publisher transmite mesaje, iar 4 Receiveri primesc aceste mesaje. (2 NodeJS si 2 C#)
Se observa un grad de distributie a mesajelor echilibrat. Fiecare aplicatie primeste un numar aproximativ de mesaje.
Astfel RabbitMQ este un tool foarte eficient in cazuri cind avem aplicatii care efectueaza acelasi lucru dar sunt scrise in limbaje diferite. Cu ajutorul lui putem distribuie niste work joburi intre aceste aplicatii.

## RabbitMQ vs AKKA.NET (Open MPI)

Plusurile la implemntarea prin RabbitMQ:
	+ Sunt cozi durabile, mesajele sunt persistate in caz de nevoie. 
	+ Putem efectua un mecanism de acknowledgment / Garantarea livrarii mesajului.
	+ Decuplarea limbajelor de programare.
	+ Mesajele se distribuie automat.
	
Plusurile la implemntarea prin AKKA.NET:
	+ Intr-un proces sunt mai multi actori (threaduri). Pe cind in cazul cu RabbitMQ un proces proceseaza un mesaj la un moment dat de timp.
	+ Actorii sunt foarte ieftin de creat.
	+ Totusi a fost mai usor de sincronizat actorii decit aplicatiile in cazul cu RabbitMQ
	+ Se scaleaza mai usor.


