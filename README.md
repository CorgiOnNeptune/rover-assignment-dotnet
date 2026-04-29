# IRISNDT Technical Evaluation - Rover Problem

See [the full assignment](docs/assignment.md) for more details on requirements.

## Run Project Locally

TODO: Include concrete instructions to run the project locally on a new machine/freshly cloned project.

## My Initial Thoughts and Plan

### The Rover Problem and Algorithm

#### The algorithm

TODO: Decide on best approach to actually looping through and handling commands given to rover(s) in a request.

#### OOP

TODO: Plan out models and the actual OO approach to the problem.

#### Design Decisions (Design Pattern for the "business logic")

TODO: Notes about approaches to the design of the data structures and models. Thinking some sort of strategy pattern at first, but maybe a bit overkill for processing the commands. Maybe a small factory or command pattern? Little more thought needed.

Probably some sort of dependency injection on the business logic side so I can pass some sort of command or simulation service onto the API.

### Tech and Requirements

1. A Web Service
   - ASP.NET Core Web API
     - RESTful design with controller-based architecture
       - POST `/api/simulation`
         - Create a simulation result.
         - Request body includes starting x, y and rovers with starting x,y,cardinal, and instructions.
         - Response: 200 | Array of finalPositions and/or a resultId to navigate to results page with visualization.
         - Either this or the next endpoint will handle saving to JSON.
       - POST `/api/results/{resultId}`
         - This will need to include image binary and base64 to "upload" the image to the web service and create the actual object in the `history.json`
         - 201 Created
       - GET `/api/results`
       - GET `/api/results/{resultId}`

     - History of all inputs/outputs stored into a JSON "db" file
       - (No point doing a full DB for this scale of a non-production project)

2. An ASP.NET MVC or Core MVC app
   - ASP.NET Core MVC
     - 3 routes
       - root `/` or `/simulation`
         - Landing home page with simulations input.
       - `/results`
         - History of inputs and outputs.
       - `/results/{resultId}`
         - Specific results from a run. (Taken to this page directly after submitting a run.)
     - Easy to use UI.
       - Simple input forms, easy for me and the user.
       - Either raw html/css or simple library like Bootstrap just for quick development.
     - Forms send all input to the API rather than handling input and calculations on the frontend side.
     - Plateau visualization
       - TBD; SVG or HTML/CSS ? The project requirements want the actual screenshot processed by the API regardless.
     - "Screenshot"
       - Client-side handling. Either JS library, native browser API? Or ASP.NET MVC equivalent to call on a client-side operation ?

#### Approach Order

1. Solve the actual problem and build out the "simulator" first.
   - Just the core algorithm, models, and functional results.
2. Build the API endpoints.
   - Adapt the simulator, make sure it can take in the appropriate expected input from a user.
3. Build the UI
   1. Create the forms for user input `/` || `/simulation`
      - Default form with "raw" input that just takes in the text input as detailed in the project specs.
        - Submit form sends to API and then page loads into `/results/{resultId}`
      - Stretch: Enhance form to also have options to use dropdown or per-detail inputs. (Inputs or dropdown for starting position, options to add rovers, etc.)
   2. Create `/results/{resultsId}` page to see results per submission
      - Start with just the raw output of the rover's final positions after receiving the inputs to move.
      - Includes plateau visualization.
        - Unsure yet of visual approach. Either SVG approach or creating a grid with HTML/CSS.
      - "Screenshot" of the plateau.
        - Unsure of library or tool to use in ASP.NET, require some research.
        - Save the screenshot to "server" directory and then save the path to `history.json` for easy access in `/results` route on frontend.
   3. Create the `/results` page to see past operations.
      - Table showing inputs and outputs of each run, as well as an option to either view/download the screenshot. Also link to the `/results/{resultId}` resource page per result.

#### Project Structure

```
.
├── RoverSolution/
│   ├── Rover.Web/
│   │   └── wwwroot/
│   │       ├── assets/
│   │       │   └── screenshots
│   │       └── ...
│   ├── Rover.Api/
│   │   └── ...
│   └── Rover.Core/
│       ├── Models
│       ├── Interfaces
│       ├── Services
│       └── ... (Class Library; shared business logic and models)
├── db/
│   └── history.json
├── docs/
│   └── ...
└── README.md
```

#### Request Flow

1. User submits simulation request through the UI
2. UI calls the API (POST `/api/simulation`)
3. Validate request
4. Some service from Rover.Core executes logic
5. A different service also saves initial results (separation of concerns here)
6. API returns response (Final positions and resultId at least)
7. UI renders the visualization
8. UI sends a screenshot back through the API (PUT or PATCH `/api/results/{resultId}`... This is PUT/PATCH so the result in history gets updated with the screenshot path.)
