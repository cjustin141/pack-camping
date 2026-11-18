# Project Plan

## Purpose and scope

Build a camping trip planner that a small group can use to coordinate dates, daily plans, and shared gear. Use the project to deepen backend skills while delivering a complete, usable application.

This is a proposed baseline. The stack is provisional; the requirements below define the initial target. Unchecked items are planned work, not completed features.

## 1. MVP requirements

### Accounts and access

- [ ] Users can register, sign in, and sign out using an established authentication framework.
- [ ] Users can view only trips they belong to.
- [ ] Each trip has one owner and zero or more other members.
- [ ] The owner can edit trip details, manage membership, and delete the trip.
- [ ] Members can view the trip, edit its shared itinerary and gear requirements, and manage their own gear claims.
- [ ] Authorization is enforced by the API for every protected operation, including requests made directly without the UI.

### Trip management and invitations

- [ ] Create a trip with a name, destination, start date, end date, and optional notes.
- [ ] List upcoming and past trips for the current user.
- [ ] Reject an end date earlier than the start date.
- [ ] Owners can generate an expiring, revocable invite link to share manually.
- [ ] A signed-in user can join through a valid invite; joining twice does not create duplicate membership.
- [ ] Members can leave and owners can remove members; departure releases that member's gear claims in the same transaction.
- [ ] An owner cannot leave without deleting the trip in the MVP; ownership transfer is deferred.

### Daily itinerary

- [ ] Add, edit, reorder, and delete itinerary entries within a trip.
- [ ] Each entry has a date, title, optional time, location, and notes.
- [ ] Display entries grouped by day and reject dates outside the trip's date range.
- [ ] Block date changes that would strand existing entries outside the trip until those entries are moved or removed.

### Personal gear catalog

- [ ] Users can create, edit, and delete personal gear entries with a name, category, owned quantity, and optional notes.
- [ ] Owned quantities must be positive integers.
- [ ] Users can add a trip requirement using a catalog entry or enter an item manually.
- [ ] Copy catalog details into the trip requirement so later catalog edits or deletion do not alter an existing trip plan.
- [ ] Catalog quantities are a planning aid; checking equipment availability across overlapping trips is outside the MVP.

### Shared packing list

- [ ] Each trip requirement has an item name, category, required quantity, and optional notes.
- [ ] Members can claim a positive integer quantity and adjust or release their own claims.
- [ ] Multiple members can contribute toward one requirement.
- [ ] Display required, claimed, and remaining quantities and who is bringing each item.
- [ ] A member can mark their entire claim packed or unpacked; changing its quantity resets it to unpacked.
- [ ] Reject reducing a requirement below its already-claimed quantity.
- [ ] Deleting a requirement also removes its claims, with an explicit confirmation in the UI.

### Concurrent changes and data integrity

- [ ] Total claimed quantity must never exceed required quantity, including during simultaneous requests.
- [ ] Protect quantity checks and claim writes with a database transaction and a suitable locking or atomic-update strategy.
- [ ] When two users request the last available unit, exactly one succeeds; the other receives a clear conflict response and refreshed availability.
- [ ] Enforce one claim per member per requirement, valid foreign keys, and positive quantities in the database.
- [ ] Use version checks for shared itinerary and requirement edits so stale writes do not silently overwrite newer changes.
- [ ] Handle duplicate submissions safely, using constraints and idempotent operations where appropriate.

### Usability and reliability

- [ ] Provide usable phone and desktop layouts, labeled form fields, and keyboard-accessible controls.
- [ ] Show loading, empty, success, validation, and failure states for the main workflows.
- [ ] Keep secrets out of source control and provide an example environment configuration without credentials.
- [ ] Record useful server errors without logging passwords, session tokens, or invite tokens.

## 2. Proposed stack checklist

Choose one option per layer and keep the architecture small. Versions should be selected and pinned during setup.

| Layer | Proposed technology | Purpose |
| --- | --- | --- |
| Frontend | React + TypeScript + Vite | Typed component-based UI |
| Styling | Tailwind CSS | Responsive layouts and consistent styling |
| API | C# + ASP.NET Core Web API | Explicit API boundary and backend business rules |
| Persistence | PostgreSQL + Entity Framework Core | Relational schema, migrations, queries, and transactions |
| Authentication | ASP.NET Core Identity with cookie authentication | Established account and password handling |
| Frontend tests | Vitest + React Testing Library | Test important user interactions |
| Backend tests | xUnit with a real PostgreSQL test database for integration tests | Verify authorization, constraints, and concurrency |
| End-to-end tests | Playwright | Exercise the main trip-planning flow |
| Local environment | Docker Compose | Reproducible database and application services |
| CI | GitHub Actions | Build, lint, type-check, and test changes |
| Hosting, later | AWS; exact services to be selected | Deployment and cloud operations practice |

- [ ] Confirm the stack before scaffolding the application.
- [ ] Keep frontend, backend, tests, and documentation in a single repository with clear directory boundaries.
- [ ] Define and document the API contract with OpenAPI.
- [ ] Prefer a same-origin deployment for the UI and API; configure secure cookies and CSRF protection for cookie-authenticated writes.
- [ ] Add reproducible local setup instructions, migrations, and sample development data.
- [ ] Add automated checks for pull requests.

## 3. Data model targets

| Entity | Responsibility and relationships |
| --- | --- |
| User | Account identity; owns catalog entries and participates in trips |
| Trip | Destination, dates, notes, and owner |
| TripMember | Joins users to trips; unique user/trip pair |
| TripInvite | Trip invitation with token hash, expiry, and revocation state |
| ItineraryEntry | Dated and ordered activity belonging to a trip |
| GearItem | Reusable catalog entry owned by a user |
| TripGearRequirement | Trip-specific item details and required quantity |
| GearClaim | Quantity and packed status assigned to a member for a requirement |

- [ ] Sketch the entity relationships before implementing migrations.
- [ ] Define deletion behavior and indexes for common queries.
- [ ] Ensure a claim's member and requirement belong to the same trip.
- [ ] Document key invariants and how the database and service layer enforce them.

## 4. Skills checklist

Check an item when you can explain the approach, implement it, and demonstrate it working.

### Core priorities

- [ ] **Relational modeling:** design relationships, foreign keys, unique constraints, indexes, and migrations.
- [ ] **API design:** use clear resources, request/response models, validation, status codes, and consistent errors.
- [ ] **Backend structure:** separate HTTP handling, business rules, and data access without unnecessary abstraction.
- [ ] **Authentication and authorization:** distinguish identity from permissions and enforce trip-level access.
- [ ] **Transactions and concurrency:** explain a race condition and demonstrate why the chosen claim strategy prevents overclaiming.
- [ ] **Frontend integration:** handle server data, forms, request failures, and conflict recovery.
- [ ] **Testing:** verify important business rules and multi-user behavior, including failure cases.
- [ ] **Git and documentation:** use focused commits and record meaningful design decisions.

### Delivery priorities

- [ ] **Docker:** build and run the application with environment-based configuration.
- [ ] **CI/CD:** automate validation, then add a deployment workflow when a deployment target exists.
- [ ] **Cloud fundamentals:** deploy the UI, API, and database with HTTPS, restricted database access, and managed secrets.
- [ ] **Operations:** add health checks and useful logs, and document migrations, backups, and recovery.
- [ ] **SQL performance:** inspect generated queries and use query plans to investigate a measured bottleneck.

### Learning approach

- [ ] Write down the expected behavior and sketch the data flow before implementing each feature.
- [ ] Build one complete feature from database to UI before expanding scope.
- [ ] Use assistance for explanations, reviews, and targeted debugging; be able to explain code before keeping it.
- [ ] Keep short notes on tradeoffs, bugs, and lessons that could support a technical interview discussion.

## 5. Build milestones

1. **Foundation:** confirm the stack, create the monorepo, connect the API to PostgreSQL, and establish migrations and CI.
2. **First complete flow:** implement authentication, trip creation, trip listing, and trip-level authorization.
3. **Collaboration:** add invitations, membership, and the daily itinerary.
4. **Gear coordination:** add the catalog, requirements, claims, packing status, and transaction-safe quantity limits.
5. **Validation and polish:** test permissions and concurrency, complete responsive states, and document local setup.
6. **Deployment:** select hosting, deploy a working demo, and document operational basics.

## 6. MVP acceptance checklist

- [ ] A fresh checkout can run using documented steps and sample configuration.
- [ ] Two users can create/join a trip, edit its itinerary, and coordinate required gear.
- [ ] A non-member cannot read or modify that trip through direct API requests.
- [ ] Simultaneous claims for the final unit cannot overclaim it, verified against PostgreSQL.
- [ ] Stale shared edits produce a useful conflict rather than silently losing changes.
- [ ] Leaving or removing a member releases their claims consistently.
- [ ] Automated checks pass and the main workflow has an end-to-end test.
- [ ] The README accurately describes completed functionality and how to run it.

## 7. Deferred features

Consider these only after the core workflow is usable:

- Meal planning and shared grocery quantities.
- Shared expenses and cost splitting.
- Maps, routes, weather, and external campsite information.
- Reusable trip templates and downloadable packing lists.
- Live updates via WebSockets or server-sent events.
- Email notifications and reminders.
- File or photo uploads, potentially using object storage.
- Equipment availability across overlapping trips.
- Ownership transfer and more granular permissions.

Campsite booking, payments, microservices, Kubernetes, and AI features are outside the initial scope.
