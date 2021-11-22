# README #

Welcome to the LeaveWizard Weather Forecasting project. 
 

### What is this repository for? ###

* This is a dummy project containing an example of how to configure Specflow with Autofac running against a .NET 5 Web API.

### How do I get set up? ###

Simply hit F5 to run the API.

### SpecFlow Configuration Explained  ###

The API has been configured to use Autofac (see Program.cs for where this is registered). 

The Startup.cs file also requires a ConfigureContainer method - this is where the dependency injection magic happens. 
Here we call a virtual method called RegisterDependencies which allows the SpecFlowStartup.cs file to override this and inject test dependencies.

The AppHostingContext.cs is responsible for managing the SpecFlowApplicationFactory which in turn setting the testing host environment.

#### Registering Dependencies ####

If you want to register new dependencies, you can do this in the Core/DependencyRegistrar.cs file.

#### Mock Builders, Fakes and Contexts Examples ####

The project contains examples of how to setup mock builders to use fakes which use context objects to setup contextual state for each test and validate results against the state.

Take a look at the WeatherPrediction.feature file and associated WeatherPredictionSteps.cs file for an example of how this works.

#### Example Transforms ####

You can find same example step argument transforms in the Transforms/BooleanTransforms.cs file.


### Contribution guidelines ###

* Tests should be written using SpecFlow for behaviour based testing and NUnit for lower level testing where required.

### Who do I talk to? ###

* rich@leavewizard.com