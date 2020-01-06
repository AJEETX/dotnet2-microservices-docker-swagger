# aspnet.core microservices ![GitHub release](https://img.shields.io/github/release/ajeetx/dotnet2-microservices-docker-swagger.svg?style=for-the-badge) ![Maintenance](https://img.shields.io/maintenance/yes/2020.svg?style=for-the-badge)

### dotnet2-microservices-docker-swagger

[![.Net Framework](https://img.shields.io/badge/DotNet-2.1_Framework-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)  | ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/dotnet2-microservices-docker-swagger.svg) | ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/dotnet2-microservices-docker-swagger.svg) |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/dotnet2-microservices-docker-swagger.svg) | [![.Net Framework](https://img.shields.io/badge/docker-install-blue.svg)](https://www.docker.com/get-started)
| --- | ---          | ---        | ---      | --- |

---------------------------------------

 #### Please see the demo below

 <img width="100%" src="Record-Point-Demo.gif" />


## Repository codebase
 
The repository consists of projects as below:


| # |Project Name | Project detail | comments | 
| ---| ---  | ---           | ---          | 
| 1 | gateway.api | Asp.Net Core2 WebApi as gateway  |  gateway to the microservices api |
| 2 | authorize.service | Asp.Net Core2 WebApi as microservice  |  create, read, delete customer | 
| 3 | authorize.service.Test | Unit Test for authorize.service |  unit test for authorize project | 
| 4 | helpdesksupport.service | Asp.Net Core2 WebApi as microservice  |  create, read, delete helpdesk tickets |
| 5 | helpdesksupport.service.Test | Unit Test for helpdesksupport.service |  unit test for helpdesksupport project |  
| 6 | docker-compose  | docker compose project | run the list of dockers of projects |

### Design structure 
 
 
    gateway.api --> [microservices]

                    1. authorize

                    2. helpdesk
 

### Summary

The overall objective of the applications :
```
>	A customer can Register
>	Then the customer can Login 
>	Authentication token is used
>	Once logged-in, user can do "CRUD" operation on Helpdesk tickets
```

### Setup detail
 
> If docker is not installed please install `docker` on your machine.

> Please download / clone the repository.

> Open the solution file through VS2017.

> Within VS2017, in the `solution folder` Set Startup project as `docker-compose` project.

> Run the solution by pressing they key `F5`
 
### Support or Contact

Having any trouble? Please read out this [documentation](https://github.com/AJEETX/dotnet2-microservices-docker-swagger/blob/master/README.md) or [contact](mailto:ajeetx@email.com) and to sort it out.

```
keep coding ;)
```

<a href="https://info.flagcounter.com/VOMj">
<img src="https://s01.flagcounter.com/count/VOMj/bg_FFFFFF/txt_000000/border_CCCCCC/columns_8/maxflags_12/viewers_0/labels_1/pageviews_1/flags_0/percent_0/" alt="Flag Counter" border="0">
</a>
