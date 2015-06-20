(function() {
  var app = angular.module('gemStore', []);

  app.controller('StoreController', function(){
    this.products = gems;
  });

  app.controller('TabController', function(){
    this.tab = 1;

    this.setTab = function(newValue){
      this.tab = newValue;
    };

    this.isSet = function(tabName){
      return this.tab === tabName;
    };
  });

app.controller('ReviewController', function() {
    this.review = {};

    this.addReview = function(product) {
      product.reviews.push(this.review);

      this.review = {};
    };
  });


  app.controller('GalleryController', function(){
    this.current = 0;
    this.setCurrent = function(newGallery){
      this.current = newGallery || 0;
    };
  });

  var gems = [
    {
      name: 'Селика',
      description: "Квалитет, достоинство, резултат.",
      description2: "Нашата компанија ви нуди обучени инструктори со долгогодишно возачко искуство кои ќе квалитетно и достојно ќе ве обучат да управувате со било кој тип на возило во временски ограничени термини кои на вас ви одговараат.",      
      price: 338, 
      priceTab: "15.500 денари / 36 часа", 
      priceTab2: "1 час 560 денари",
      images:["images/selika.gif"],
      imagesThumb: ["imagesThumb/kola1.jpg",
                    "imagesThumb/kola4.jpg",
                    "imagesThumb/kola10.jpg",
                    "imagesThumb/kola15.jpg",
                    "imagesThumb/kola19.jpg",
                    "imagesThumb/kola3.jpg",
                    ],
    reviews: [{
        stars: 2,
        body: "Цената е достапна, меѓутоа знам автошколи со многу подобри инструктори кои навистина ќе ти обрнат вниманиe. Положив од трети пат, а другар ми од втори пат.",
        author: "gjorgji_123@example.org",
        createdOn: 1397490980837
      }, {
        stars: 1,
        body: "Како да не, обучени инструктори.. кој од кој се таму. Доаѓам првиот час ми вика: “Ајде вози“. Како да возам бре брат, прв пат пред волан седнувам?!",
        author: "boris_maliot@example.org",
        createdOn: 1397490980837
      }, {
        stars: 4,
        body: "Мене ми се погоди инстукторот, задоволна сум од автошколата. Положив од втор пат, али океј е, како и да е да положиш од прва е речиси невозможно.",
        author: "nat@example.org",
        createdOn: 1397490980837
      }]
     
    },
    {
      name: 'Ada',
      description: "Брзо, сигурно, загарантирано",
      description2: "Долгогодишното возачко и обучувачко искуство на нашите инструктори придонесува до квалитетните возачи кои излегуваат од нашата авто школа. Целосно запознавање со возилото, правилата за возење, функционалноста за било која категорија на возила. Клиентите имаат можност согласно со цените да изберат свое возило со кое ќе се обучуваат во текот на обуката и со кое ќе полагаат. Вашиот успех е наша цел.",  
     price: 395, 
      priceTab: "18.500 денари / 36 часа", 
      priceTab2: "1 час 590 денари",
      images:["images/ada.gif"],
        imagesThumb: ["imagesThumb/kola2.jpg",
                    "imagesThumb/kola5.jpg",
                    "imagesThumb/kola11.jpg",
                    "imagesThumb/kola16.jpg",
                    "imagesThumb/kola20.jpg",
                    "imagesThumb/kola4.jpg",
                     "imagesThumb/kola3.jpg",
                    "imagesThumb/kola15.jpg",
                    ],
        reviews: [{
        stars: 3,
        body: "Абе океј е автошколава, ама за тиа пари може и боље да се најде, пример АМСМ или некоја...",
        author: "turtleguyy@example.org",
        createdOn: 1397490980837
      }, {
        stars: 1,
        body: "Брзо, сигурно... мижи асам да ти баам..",
        author: "loren_93@example.org",
        createdOn: 1397490980837
      }, {
        stars: 1,
        body: "Ако ме прашувате мене не си ги трошете парите на оваа автошкола. Знам дека во другите автошколи многу попрофесионално се бават со клиентите.",
        author: "nat@example.org",
        createdOn: 1397490980837
      }]
    },
    {
      name: 'Џовани',
      description:"Достапност, знаење и искуство",
      description2: "Ви нудиме обучени инструктори со големо искуство околу поучување на нашите клиенти. Умеењето да се пренесе знаењето е наша главна цел за да имаме бројни положени клиенти без дополнителни полагања.  Вие го бирате возилото и термините кои ви одговараат, а ние ви овозможуваме инструктор кој професионално си ја извршува работата.",      
        price: 382, 
      priceTab: "17.500 денари / 36 часа", 
      priceTab2: "1 час 560 денари",
      images:["images/dzovani.gif"],
      imagesThumb: ["imagesThumb/kola3.jpg",
                    "imagesThumb/kola6.jpg",
                    "imagesThumb/kola12.jpg",
                    "imagesThumb/kola17.jpg",
                    "imagesThumb/kola1.jpg",
                    "imagesThumb/kola5.jpg",
                    "imagesThumb/kola16.jpg",
                    "imagesThumb/kola13.jpg",
                    ],
      reviews: [{
        stars: 5,
        body: "Првин не ми се погоди инструкторот. Седнувам прв час и се понашаше ко ја да знам да возам, не да земе да објасни, ми вика палиш мотор, даваш гас, кумплук ова она и си возиш, па барав замена и ми дадоа нова инструкторка. Таа си беше океј човек, се убаво ми објасни, ги завршив часовите и положив од прва. :)",
        author: "rozita_flawness@example.org",
        createdOn: 1397490980837
      }, {
        stars: 3,
        body: "Онака се, океј се..",
        author: "anna_74@example.org",
        createdOn: 1397490980837
      }, {
        stars: 1,
        body: "Ако ме прашувате мене не си ги трошете парите на оваа автошкола. Знам дека во другите автошколи многу попрофесионално се бават со клиентите.",
        author: "noni_nino@example.org",
        createdOn: 1397490980837
      }]
    },

    {
      name: 'M 2 2',
      description: "На правото место!",
      description2: "Како едно од првите формирани автошколи во Скопје и во Македонија од наша страна можеме да ви гарантираме квалитет, достапност и расположивост, Ваша увереност и сигурност, знаење и професионално искуство од ова поле. Имате можност да изберете инструктор и кола, наведете за која категорија сакате да ве обучиме и ние сме секогаш тука за вас. ",      
      price: 373, 
      priceTab: "17.000 денари / 36 часа", 
      priceTab2: "1 час 520 денари",
      images:["images/m22.gif"],
      imagesThumb: ["imagesThumb/kola4.jpg",
                    "imagesThumb/kola7.jpg",
                    "imagesThumb/kola13.jpg",
                    "imagesThumb/kola18.jpg",
                    "imagesThumb/kola2.jpg",
                    "imagesThumb/kola6.jpg",
                    ],
    reviews: [{
        stars: 4,
        body: "Цената е достапна, положив од четврти пат, ама реално ја секад се бунев. Малце тешко идеше договарањето на термини, бидејќи инструкторот ми беше зафатен човек, ама топ си беше.",
        author: "nikola2_123@example.org",
        createdOn: 1397490980837
      }, {
        stars: 1,
        body: "Како да не, обучени инструктори.. кој од кој се таму. Доаѓам првиот час ми вика: “Ајде вози“. Како да возам бре брат, прв пат пред волан седнувам?!",
        author: "boris_maliot@example.org",
        createdOn: 1397490980837
      },{
        stars: 4,
        body: "Цената е достапна, положив од четврти пат, ама реално ја секад се бунев. Малце тешко идеше договарањето на термини, бидејќи инструкторот ми беше зафатен човек, ама топ си беше.",
        author: "eleonora_97@example.org",
        createdOn: 1397490980837
      }, {
        stars: 5,
        body: "Ептен сум задоволен од часовите што ги земав во автошколата, мене ми се погоди инстукторот. Положив од прв пат, ама земав и неколку дополнителни часови пред да полагам.",
        author: "andrei_petrov@example.org",
        createdOn: 1397490980837
      }]
    },

    {
     name: 'Мартин',
      description: "Автошкола Мартин - секогаш тука за вас!",
     description2: "На располагање ви се инструктори обучени по највисоките стандарди чии клиенти се главната окупација. Достапни во секое за Вас расположиво време ќе ви овозможат незаменливо искуство. Искуството и докажаната способност за појаснување придонесува за многуте квалитетни возачи.",
     price: 425, 
      priceTab: "19.500 денари / 36 часа", 
      priceTab2: "1 час 560 денари",
      images:["images/martin.gif"],
      imagesThumb: ["imagesThumb/kola5.jpg",
                    "imagesThumb/kola8.jpg",
                    "imagesThumb/kola14.jpg",
                    "imagesThumb/kola19.jpg",
                    "imagesThumb/kola3.jpg",
                    "imagesThumb/kola7.jpg",
                    ],
    reviews: [{
        stars: 4,
        body: "Цената е достапна, положив од четврти пат, ама реално ја секад се бунев. Малце тешко идеше договарањето на термини, бидејќи инструкторот ми беше зафатен човек, ама топ си беше.",
        author: "gjorgji_123@example.org",
        createdOn: 1397490980837
      }, {
         stars: 1,
        body: "Ако ме прашувате мене не си ги трошете парите на оваа автошкола. Знам дека во другите автошколи многу попрофесионално се бават со клиентите.",
        author: "nat@example.org",
        createdOn: 1397490980837
      }]
    },
    {
     name: 'Мобилити',
      description: "Мобилити - решение за Вас!",
      description2: "Нашата компанија ви нуди обучени инструктори со долгогодишно возачко искуство кои ќе квалитетно и достојно ќе ве обучат да управувате со било кој тип на возило во временски ограничени термини кои на вас ви одговараат.",      
      price: 349, 
      priceTab: "16.000 денари / 36 часа", 
      priceTab2: "1 час 444 денари",
      images:["images/mobiliti.gif"],
      imagesThumb: ["imagesThumb/kola9.jpg",
                    "imagesThumb/kola15.jpg",
                    "imagesThumb/kola20.jpg",
                    "imagesThumb/kola2.jpg",
                    "imagesThumb/kola18.jpg",
                    "imagesThumb/kola15.jpg",
                    "imagesThumb/kola16.jpg",
                    "imagesThumb/kola13.jpg",
                    ],
          reviews: [{
        stars: 2,
        body: "Цената е okej, но дек си имат автошколи со подобри инструктори си имат. Положив од трети пат.",
        author: "evgenij_123@example.org",
        createdOn: 1397490980837
      }, {
        stars: 1,
        body: "Абееее секогаш тука за мене, немат врска тоа, да си знајте",
        author: "sandra_87@example.org",
        createdOn: 1397490980837
      }, {
        stars: 4,
        body: "Мене ми се погоди инстукторот, задоволна сум од автошколата. Положив од втор пат, али океј е, како и да е да положиш од прва е речиси невозможно.",
        author: "natali@example.org",
        createdOn: 1397490980837
      }]
    },
    {
     name: 'Нова',
      description: "Нова - Вашата автошкола",
       description2: "Долгогодишното возачко и обучувачко искуство на нашите инструктори придонесува до квалитетните возачи кои излегуваат од нашата авто школа. Целосно запознавање со возилото, правилата за возење, функционалноста за било која категорија на возила. Клиентите имаат можност согласно со цените да изберат свое возило со кое ќе се обучуваат во текот на обуката и со кое ќе полагаат. Вашиот успех е наша цел.",      
       price: 448, 
      priceTab: "20.500 денари / 36 часа", 
      priceTab2: "1 час 600 денари",
      images:["images/nova.gif"],
      imagesThumb: ["imagesThumb/kola17.jpg",
                    "imagesThumb/kola14.jpg",
                    "imagesThumb/kola20.jpg",
                    "imagesThumb/kola16.jpg",
                    "imagesThumb/kola13.jpg",
                    "imagesThumb/kola11.jpg",
                    "imagesThumb/kola18.jpg",
                    "imagesThumb/kola9.jpg",
                    ],
        reviews: [{
       stars: 5,
        body: "Првин не ми се погоди инструкторот. Седнувам прв час и се понашаше ко ја да знам да возам, не да земе да објасни, ми вика палиш мотор, даваш гас, кумплук ова она и си возиш, па барав замена и ми дадоа нова инструкторка. Таа си беше океј човек, се убаво ми објасни, ги завршив часовите и положив од прва. :)",
        author: "tanya_vilevska@example.org",
        createdOn: 1397490980837
      }, {
        stars: 4,
        body: "Мене ми се погоди инстукторот, задоволна сум од автошколата. Положив од втор пат, али океј е, како и да е да положиш од прва е речиси невозможно.",
        author: "natali@example.org",
        createdOn: 1397490980837
      }, {
        stars: 4,
        body: "Malce e skapo ama vredi,dobri se.",
        author: "vladimir_sukarev@example.org",
        createdOn: 1397490980837
      }]
    },
    {
     name: 'Тримоски',
      description: "Сигурно и загарантирано",
      description2: "Ви нудиме обучени инструктори со големо искуство околу поучување на нашите клиенти. Умеењето да се пренесе знаењето е наша главна цел за да имаме бројни положени клиенти без дополнителни полагања.  Вие го бирате возилото и термините кои ви одговараат, а ние ви овозможуваме инструктор кој професионално си ја извршува работата.",      
      price: 339, 
      priceTab: "15.500 денари / 36 часа", 
      priceTab2: "1 час 430 денари",
      images:["images/trimoski.gif"],
      imagesThumb: ["imagesThumb/kola11.jpg",
                    "imagesThumb/kola15.jpg",
                    "imagesThumb/kola9.jpg",
                    "imagesThumb/kola1.jpg",
                    "imagesThumb/kola13.jpg",
                    "imagesThumb/kola16.jpg",
                    ],
    reviews: [{
       stars: 5,
        body: "Ej top avtoshkola, tuka da odite, instruktorot razbran covek, tie glavnite tamu isto, Mnogu sum zadovolna. Duri eden  cas gratis mi dadoa :)",
        author: "maya_timber@example.org",
        createdOn: 1397490980837
      }, {
        stars: 4,
        body: "Мене ми се погоди инстукторот, задоволна сум од автошколата. Положив од втор пат, али океј е, како и да е да положиш од прва е речиси невозможно.",
        author: "natali@example.org",
        createdOn: 1397490980837
      }, {
        stars: 4,
        body: "Ne e skapo i plus vredi, jas dobro projdov.",
        author: "diana_kursalina@example.org",
        createdOn: 1397490980837
      }]

    },
    {
     name: 'Зебра',
      description: "Автошкола Зебра - секогаш тука за Вас",
      description2: "На располагање ви се инструктори обучени по највисоките стандарди чии клиенти се главната окупација. Достапни во секое за Вас расположиво време ќе ви овозможат незаменливо искуство. Искуството и докажаната способност за појаснување придонесува за многуте квалитетни возачи.",
   
        price: 382, 
      priceTab: "17.500 денари / 36 часа", 
      priceTab2: "1 час 490 денари",
      images:["images/zebra.gif"],
      imagesThumb: ["imagesThumb/kola10.jpg",
                    "imagesThumb/kola16.jpg",
                    "imagesThumb/kola20.jpg",
                    "imagesThumb/kola5.jpg",
                    "imagesThumb/kola17.jpg",
                    "imagesThumb/kola15.jpg",
                     "imagesThumb/kola11.jpg",
                    "imagesThumb/kola4.jpg",
                    ],
     reviews: [{
        stars: 2,
        body: "Цената е okej, но дек си имат автошколи со подобри инструктори си имат. Положив од трети пат.",
        author: "evgenij_123@example.org",
        createdOn: 1397490980837
      },  {
        stars: 4,
        body: "Мене ми се погоди инстукторот, задоволна сум од автошколата. Положив од втор пат, али океј е, како и да е да положиш од прва е речиси невозможно.",
        author: "natali@example.org",
        createdOn: 1397490980837
      }]

    }

  ];
})();
