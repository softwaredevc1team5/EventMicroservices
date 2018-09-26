![Eventhub](https://github.com/softwaredevc1team5/EventMicroservices/blob/LastAssignment3b/Code/WebMvc/wwwroot/images/EventHubLogo2.png)


   Is an event manager website that allows users to create, browse and promote events. It was built using Microservices architecture, Docker Linux containers, Swagger, Redis, RabbitMQ, IdentityServer4, ASP.NET Core and C#.

 - All the project is Dockerized using 8 containers:
	- EventCatalogApi
	- WishListApi
	- EventTicketApi
	- TokenServiceApi
	- OrderApi
	- WebMvc
	- Mssqlserver
	- Redis


# Asignments

# Assignment 3c
Video: https://youtu.be/fbL0-igfIB0

- Communication between services via messaging using RabbitMQ
- Microservice Wishlist was modified and now is using use as cache Redis to save the user's wishlist.
- Microservice Order was added to the project
- Pages added to the project:
	- Orders Page
		- Upcoming orders
		- Past orders
		- Canceled orders
	

# Assignment 3b
Video: https://youtu.be/7K1khtbUwTM

- All the project is Dockerized we are using 6 containers:
	- eventcatalog
	- wishlist
	- eventticket
	- tokenservice
	- webmvc
	- mssqlserver

- The TokenServiceAPI is integrated in the application and the user can register, login and logout from the app.

- The WebApp has 4 pages:
	- Main Page 
	- Event Detail 
	- Thing to do in a City
	- Search Anything
	
