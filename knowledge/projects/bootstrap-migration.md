---
title: Responsive Bootstrap Modernization of Large-Scale School CMS

organization: Moava AS

role: Fullstack Developer

period:
  from: 2013-11
  to: 2014-11

status: completed

technologies:
  - php
  - javascript
  - bootstrap-3
  - css
  - html

concepts:
  - frontend-architecture
  - legacy-modernization
  - responsive-design
  - mobile-first
  - bootstrap-grid
  - cms
  - multi-tenant
  - backwards-compatibility
  - progressive-rollout
  - feature-flag
  - legacy-browser-support
  - data-migration
  - algorithm-design
  - user-experience
  - accessibility
  - universal-design
  - technical-debt
  - customer-retention
  - customer-satisfaction

links:
  github:
  live:

---

# Overview

As part of a major modernization of Moava AS's school CMS, I took primary responsibility for transforming the customer-facing websites from a fixed-width, desktop-oriented design into a responsive Bootstrap 3-based frontend.

Moava provided a SaaS CMS used by approximately 1,300 schools in Norway. Teachers and principals used the CMS to create and manage their own school websites, with a high degree of autonomy. Customers generally managed their own content and configuration and only contacted Moava for support when they needed assistance.

The existing frontend had been developed over many years and followed a layout approach that was closer to the web of the late 1990s than modern responsive web development. It relied on fixed-width desktop layouts, tables for layout, fixed pixel dimensions, custom CSS, inline styling, a fixed central content area, and relatively few consistent global design rules. It also had limited consideration for modern accessibility, mobile usability, and touch interaction.

The goal was not to redesign a single website. The CMS dynamically generated the public websites of approximately 1,300 schools, each of which could use more than 30 different modules such as navigation, news, articles, article lists, images, image galleries, video, slideshows, employees, email lists, forms, tables, calendars, search, and content blocks.

A typical school had at least 30 generated pages, giving a conservative baseline of approximately 39,000 dynamically generated pages across the customer base. The important architectural point was that these pages did not need to be migrated individually. The reusable page scaffolding and CMS modules that generated them needed to be modernized.

I was the product owner and primary developer for the frontend modernization. Working as part of a team of developers, I personally worked through all of the 30+ CMS modules involved in the project, collaborating with the other developers when I needed input or assistance.

The modernization required the old and new frontend to operate simultaneously. The same underlying customer content and database data had to work with both versions, so a school could remain on the old design while another school used the new responsive design.

I implemented a per-school frontend switch in the database, controlled through a restricted administration interface available to Moava employees. This allowed the team to enable the new design for individual schools, progressively roll it out, and revert a school to the old design immediately if necessary without migrating or recreating any content.

The responsive implementation covered the entire frontend architecture and the individual CMS modules. It introduced Bootstrap's 12-column grid, responsive navigation, mobile column stacking and prioritization, responsive images and tables, responsive typography, redesigned headers and banners, touch-friendly interaction, and mobile-specific module behavior.

One particularly difficult part of the project was converting approximately 1,000 existing customer column configurations. Customers had historically been allowed to enter free-form column widths, including percentages, pixel values, mixed units, incomplete configurations, and values that technically should not have worked but happened to render acceptably because of browser and CSS behavior. I analyzed the existing configurations, identified six recurring patterns, and created an algorithm to convert them into appropriate Bootstrap 12-column layouts.

I also redesigned the customer-facing column configuration controls. Instead of allowing arbitrary text values, the new interface used a multi-handle custom grid slider with one to three handles depending on the number of columns. Handles snapped to the 12 available Bootstrap grid steps, allowing administrators to visually configure column proportions while remaining within the constraints of the new layout system.

The modernization was introduced through pilot customers and a gradual rollout rather than a single platform-wide migration. The team continued to maintain important bug fixes for the old frontend for a period after launch, while development focus moved to the new system. Approximately three months after the new frontend was introduced, all schools had been transitioned to the new design.

The project was driven by the growing importance of mobile access to school information such as schedules and weekly plans, as well as customer retention, customer satisfaction, product modernization, competitiveness, usability, and the need to move toward universal-design and accessibility requirements.

---

# Context

Moava AS provided a SaaS CMS used by approximately 1,300 schools in Norway.

The CMS gave schools substantial autonomy over their public websites. Teachers and principals could create and manage content themselves using the CMS, combining different reusable modules to construct their pages. Moava primarily provided the platform and support rather than manually managing the schools' content.

The CMS generated the public websites dynamically from customer-specific content and configuration stored in each school's database.

The platform contained more than 30 different CMS modules, including:

- Navigation and menus
- News
- Articles
- Article lists
- Images
- Image galleries
- Video
- Slideshows
- Employees
- Email lists
- Forms
- Tables
- Calendars
- Search
- Content blocks
- Other school-specific content modules

The existing frontend had accumulated significant technical debt.

Its layout model was based primarily on:

- Fixed-width desktop layouts.
- HTML tables used for layout.
- Fixed pixel dimensions.
- Custom CSS.
- Hardcoded inline CSS in some areas.
- A fixed central content area.
- Few consistent global design rules.
- Hard corners and other outdated visual conventions.
- Limited consideration for responsive design.
- Limited consideration for modern accessibility and universal-design requirements.

This approach had worked for years on desktop computers, but the requirements of the web had changed.

Mobile phones were increasingly being used by students to access school information such as schedules and weekly plans. The websites therefore needed to work for students, teachers, parents, and other visitors across a much wider range of devices and screen sizes.

The project was driven by several goals:

- Improve the mobile experience.
- Improve customer satisfaction.
- Support customer retention.
- Modernize the product.
- Remain competitive.
- Improve usability.
- Move the platform toward universal-design requirements.
- Improve accessibility for people with disabilities.
- Replace the limitations of the legacy frontend architecture.

The CMS generated pages dynamically, so the practical migration boundary was the page scaffolding and the reusable modules rather than individual customer pages.

With approximately 1,300 schools and at least 30 generated pages per school as a conservative baseline, this represented approximately 39,000 generated pages before accounting for the much larger number of actual pages and module combinations across the platform.

The goal was therefore to change how those pages were generated rather than manually rebuild them.

---

# Task

I joined the development team as a new developer and took primary responsibility for the implementation of the responsive frontend modernization.

I was the product owner for the project and worked closely with the other developers on the team. Although I was responsible for driving the implementation across the frontend, I collaborated with the team and asked for assistance when needed rather than working in isolation.

My responsibility covered the full frontend modernization, including the page scaffolding, the individual CMS modules, the PHP rendering architecture, responsive behavior, legacy browser compatibility, customer configuration, and rollout mechanism.

My work included:

- Reworking the frontend architecture around Bootstrap 3.
- Rebuilding the page scaffolding for responsive layouts.
- Working through all 30+ CMS modules.
- Updating the modules to generate Bootstrap-compatible HTML and CSS.
- Introducing responsive behavior across the CMS.
- Implementing responsive navigation.
- Implementing mobile-specific interaction patterns.
- Making columns stack appropriately on smaller screens.
- Introducing configurable mobile column priority.
- Making images responsive.
- Making tables usable on smaller screens.
- Adjusting typography for different screen sizes.
- Adapting headers and banners.
- Improving touch interaction.
- Supporting older browsers, particularly Internet Explorer.
- Creating legacy CSS fallbacks where necessary.
- Refactoring existing PHP toward a more object-oriented architecture.
- Separating shared application logic from frontend-specific rendering.
- Allowing old and new frontend modes to use the same underlying customer content.
- Implementing the per-school frontend-mode switch.
- Creating a restricted administration control for switching frontend modes.
- Designing an algorithm for converting inconsistent legacy column configurations.
- Analyzing approximately 1,000 existing customer configurations.
- Identifying six recurring legacy column-width patterns.
- Creating a new Bootstrap-grid-based column configuration interface.
- Testing locally and on remote test servers.
- Working with pilot customers.
- Supporting a progressive customer rollout.
- Supporting customers during the transition.
- Working with the design team to provide customers with configuration assistance.

The key architectural requirement was that existing customer content had to remain valid for both frontend versions.

Customers should not have to rebuild their pages or recreate their content simply because the presentation layer had changed.

---

# Challenge

## Challenge: Modernizing a Large CMS Instead of a Single Website

### Problem

The project involved far more than replacing a CSS file or making one website responsive.

Moava's CMS generated the public websites of approximately 1,300 schools. Each school could combine more than 30 reusable modules, and each module could appear in different page structures and configurations.

The existing frontend had been built around assumptions that were fundamentally desktop-oriented.

Fixed-width layouts, tables, pixel dimensions, custom CSS, inline styles, and module-specific layout logic were spread throughout the system.

Simply adding responsive CSS to the outer page container would not have solved the problem.

The individual modules also generated HTML that was not designed around responsive layout principles.

The challenge was therefore to modernize the entire rendering system while preserving the existing customer data.

### Constraints

The project had to satisfy several constraints simultaneously:

- Approximately 1,300 schools were already using the CMS.
- Customers had substantial autonomy over their content and configuration.
- More than 30 reusable modules could be used on customer websites.
- Each school had many dynamically generated pages.
- Existing customer content had to remain unchanged.
- Existing pages could not be individually rebuilt.
- The old and new frontend had to work simultaneously.
- One school could use the old design while another used the new design.
- Customers should not have to perform a migration themselves.
- The new design needed to be reversible.
- The frontend had to work on desktop and mobile devices.
- Older browsers, particularly Internet Explorer, still needed to be supported.
- The migration needed to avoid a large spike in support requests.
- The system needed to move toward better accessibility and universal design.

### Solution

I treated the reusable rendering layer as the migration boundary.

Instead of migrating individual pages, I modernized the scaffolding and the reusable CMS modules responsible for generating those pages.

The underlying customer content remained unchanged.

The application was restructured so that shared application logic could be reused while the presentation layer could produce either the legacy or responsive frontend.

A per-school database setting determined which frontend rendering mode was active.

This meant that two schools could use different frontend versions while using the same underlying CMS architecture and content model.

### Result

The platform could modernize thousands of dynamically generated pages without individually rebuilding them.

The same customer content could be rendered through either frontend.

This made the modernization feasible at the scale of the platform and provided a foundation for a controlled rollout.

---

## Challenge: Supporting More Than 30 Responsive CMS Modules

### Problem

The CMS contained more than 30 reusable modules, each with its own HTML structure, CSS, layout assumptions, and behavior.

Examples included:

* Navigation
* News
* Articles
* Article lists
* Images
* Galleries
* Video
* Slideshows
* Employees
* Email lists
* Forms
* Tables
* Calendars
* Search
* Content blocks

Each module needed to work within the Bootstrap layout system and behave sensibly at different viewport sizes.

Some modules also had interaction patterns that made sense on desktop but needed to be redesigned for touch devices.

### Solution

I systematically worked through all of the modules and converted their generated frontend output to work with Bootstrap 3 and responsive design principles.

This included modifying HTML structures, CSS, JavaScript behavior, and module-specific layouts where necessary.

The responsive behavior included:

* Collapsing navigation for smaller screens.
* Mobile navigation using modal-style interaction.
* Supporting the browser back button when closing mobile navigation.
* Stacking columns vertically on smaller screens.
* Allowing administrators to configure which column received priority on mobile.
* Using the middle/main column as the default priority.
* Allowing the customer to change the priority.
* Scaling images appropriately.
* Making tables usable on narrow screens.
* Adjusting typography.
* Adapting module layouts to available screen width.
* Redesigning headers and banners.
* Improving touch interaction.
* Supporting responsive forms and controls.
* Adding legacy CSS fallbacks for older browsers.

### Result

The modules became responsive building blocks rather than fixed desktop components.

Because the modules generated the pages dynamically, modernizing the modules automatically improved the responsive behavior of the large number of pages that used them.

---

## Challenge: Converting Inconsistent Legacy Column Configurations

### Problem

One of the most technically unusual parts of the project was the existing column configuration system.

Customers could configure between one and four columns using four free-form text input fields.

The fields had historically allowed customers to enter arbitrary values.

Examples included configurations such as:

* 30%
* 30% / 30%
* 20% / 1024 / 20%
* 100px / 400px / 400px / 100px
* 30% / 800px / 30%


These values were not necessarily valid or mathematically consistent.

Some configurations did not add up to 100%.

Some mixed percentages and pixels.

Some used values that were not really correct CSS dimensions.

Nevertheless, these configurations had often appeared to work because of browser behavior, CSS quirks, or bugs and assumptions in the old system.

Bootstrap 3 introduced a much more structured 12-column grid.

The existing configurations could therefore not simply be copied into Bootstrap.

### Solution

I analyzed approximately 1,000 existing customer configurations to understand how the free-form settings had actually been used.

Rather than assuming the legacy data was correct, I looked for recurring patterns in the real configurations.

I identified six recurring patterns in the existing data.

The migration algorithm then interpreted the legacy values according to factors such as:

* Number of columns.
* Whether values were expressed as percentages.
* Whether values were expressed in pixels.
* Relative numeric sizes.
* Relationships between multiple column values.
* Recurring combinations found in the existing customer data.

The algorithm converted these legacy configurations into Bootstrap-compatible widths while attempting to preserve the approximate visual proportions of the original layout.

Where the existing configuration could not be reliably mapped, standard Bootstrap widths were assigned based on the number of columns.

The goal was not to preserve invalid CSS literally.

The goal was to infer the layout the customer had intended and represent that layout using the Bootstrap 12-column system.

### Result

The conversion transformed a large set of inconsistent historical configuration data into predictable Bootstrap-compatible layouts without requiring developers to manually correct every customer configuration.

The approach also prevented the new responsive system from continuing to depend on the accidental behavior that had allowed many of the old configurations to work.

---

## Challenge: Giving Customers a Safe Way to Configure Bootstrap Columns

### Problem

The old system allowed customers to type arbitrary values into text fields.

That was incompatible with the stricter Bootstrap grid model.

Simply replacing the old text fields with new numeric inputs would still have left customers responsible for understanding Bootstrap's grid system.

### Solution

I redesigned the column configuration interface around the Bootstrap 12-column grid.

The new interface used a single sliding bar.

The number of handles depended dynamically on the selected number of columns:

* One column: the slider was disabled because no division was necessary.
* Two columns: one handle controlled the division.
* Three columns: two handles controlled the divisions.
* Four columns: three handles controlled the divisions.

The handles snapped to the 12 Bootstrap grid steps.

This meant that administrators could visually define the relative column widths while the interface itself prevented them from creating configurations outside the Bootstrap grid model.

### Result

The new configuration interface made the Bootstrap grid easier for administrators to use and prevented new invalid column configurations from being introduced through the normal administration interface.

It also reduced the need for customers to understand the technical details of Bootstrap's grid system.

---

## Challenge: Maintaining Old and New Frontends Simultaneously

### Problem

The entire customer base could not simply be switched to the new frontend at once.

A platform-wide migration would have created unnecessary operational risk.

At the same time, the old and new designs had to work against the same customer data.

A school using the old design and a school using the new design needed to coexist on the same platform.

### Solution

I implemented a per-school frontend-mode setting stored in each school's database.

The setting was exposed through the design section of the CMS administration interface, but access was restricted to Moava employees.

The PHP application checked the setting when generating the public website and selected the corresponding rendering mode.

The old and new systems were therefore not separate CMS products. They were two presentation modes operating on the same underlying customer data and application logic.

### Result

The team could enable the new design for individual schools without affecting other customers.

If a school needed to return to the old design, the team could simply switch the setting back.

No content migration was required.

No page rebuild was required.

No database conversion of the customer content was required.

This provided an immediate and practical rollback mechanism during the transition.

---

## Challenge: Rolling Out the New Design Safely

### Problem

A new responsive frontend used by approximately 1,300 schools could potentially generate a large number of customer issues if enabled everywhere at once.

The system therefore needed a controlled rollout mechanism.

### Solution

The new frontend was tested internally using local development servers and remote test servers.

After internal testing, pilot customers were used to validate the new design in real-world environments.

The rollout was then performed gradually.

The team enabled the responsive design for schools individually rather than performing a single platform-wide switch.

The old frontend remained operational during the transition, and important bug fixes were still made to the legacy frontend for a period after the new design launched.

If a customer disliked the new design or encountered a problem, the team could revert the school by changing the frontend-mode setting.

Only a small number of customers ultimately requested a rollback. After discussions with the team and assistance with their settings, those customers also agreed to keep the new version.

Moava also offered each customer one hour of free telephone design consultation with a designer. This allowed customers to get help adjusting settings, including column widths and visual elements such as banners.

### Result

The rollout could happen progressively instead of as a high-risk big-bang deployment.

The team could test the new frontend with real customers, identify problems, fix them, and continue expanding the rollout without putting all 1,300 schools at risk simultaneously.

Approximately three months after the new frontend was introduced, all schools had transitioned to the new design.

---

# Action

## Architecture

### Frontend

The frontend was redesigned around Bootstrap 3 and responsive layout principles.

The existing fixed-width page scaffolding was replaced with a Bootstrap-based grid and responsive structure.

All 30+ CMS modules were reviewed and modified so that their generated HTML, CSS, and JavaScript worked within the new frontend.

The frontend supported:

* Bootstrap 3 grid layouts.
* Responsive navigation.
* Mobile navigation.
* Modal-style mobile interactions.
* Browser back-button behavior for mobile navigation.
* Responsive columns.
* Configurable mobile column priority.
* Responsive images.
* Responsive tables.
* Responsive typography.
* Responsive forms.
* Responsive module layouts.
* Responsive headers.
* Responsive banners.
* Touch-friendly controls.
* Legacy browser fallbacks.

The same customer content could be rendered through either the legacy or responsive frontend.

### Backend

The application was primarily PHP-based.

I refactored parts of the existing PHP rendering code toward a more object-oriented architecture.

The goal was to make shared application logic reusable while separating it more clearly from presentation-specific rendering.

This allowed the old and new frontend modes to share underlying application logic rather than requiring two completely independent applications.

The PHP rendering layer used the per-school frontend-mode setting and selected the appropriate presentation mode.

### Database

Each school had its own database containing its CMS content and configuration.

The modernization did not require duplicating or migrating the customer content.

A per-school database variable controlled which frontend mode was active.

The same content could therefore be rendered through either the legacy or responsive frontend.

### Infrastructure

Development and testing were performed using local development servers and remote test servers.

Changes could be deployed to remote test environments before being committed and pushed to production.

Pilot customers were used to validate the new system under real-world conditions.

The production rollout was performed progressively.

---

## Technical Decisions

## Decision: Use Bootstrap 3 as the Responsive Layout Foundation

### Context

The existing frontend lacked a consistent responsive grid and relied heavily on fixed-width and table-based layouts.

A structured responsive system was needed across many different modules and page configurations.

### Chosen Solution

Bootstrap 3 was used as the foundation for the new responsive frontend.

The page scaffolding and CMS modules were adapted to use Bootstrap's grid and responsive conventions.

### Trade-offs

Bootstrap provided a consistent grid and responsive model, but it also meant that the existing arbitrary customer configurations had to be normalized.

The migration therefore required both frontend modernization and data-conversion logic.

---

## Decision: Keep Customer Content Independent from Frontend Presentation

### Context

Customers should not have to recreate their websites simply because the frontend design changed.

The same content needed to work with both frontend versions.

### Chosen Solution

The old and new frontends shared the same underlying customer data.

The PHP rendering layer determined which presentation mode to use based on the per-school frontend setting.

### Trade-offs

The team temporarily had to maintain two frontend presentation modes.

However, this provided:

* Backward compatibility.
* Progressive rollout.
* Immediate rollback.
* No customer content migration.
* No page-by-page rebuilding.
* Lower operational risk.

---

## Decision: Use a Per-School Feature Switch

### Context

A platform-wide frontend switch would have created too large a blast radius.

The team needed to migrate customers independently.

### Chosen Solution

A frontend-mode variable was stored in each school's database.

Only authorized Moava employees could access the corresponding control in the CMS administration interface.

The PHP rendering layer used the set value when generating the public website.

### Trade-offs

The system temporarily had to support both rendering modes.

However, the approach allowed individual schools to be enabled, tested, or reverted independently.

---

## Decision: Normalize Legacy Column Configurations

### Context

The existing column configuration system allowed free-form values such as percentages, pixels, mixed units, and technically invalid combinations.

Bootstrap required a 12-column grid.

### Chosen Solution

Approximately 1,000 customer configurations were analyzed.

Six recurring patterns were identified and used as the basis for an automated conversion algorithm.

The algorithm converted the historical configurations into Bootstrap-compatible grid widths while attempting to preserve their approximate visual structure.

### Trade-offs

Not every historical configuration could be perfectly reproduced.

Some unusual configurations required fallback behavior.

However, the approach was considerably more reliable than applying one generic formula or manually fixing every customer configuration.

---

## Decision: Replace Free-Form Column Inputs with a Multi-Handle Custom Grid Slider

### Context

The old administration interface allowed customers to enter arbitrary column-width values.

This was one of the reasons inconsistent configurations had accumulated.

### Chosen Solution

The new interface used a multi-handle custom slider representing the Bootstrap 12-column grid.

The slider dynamically changed the number of handles based on the selected number of columns.

For example:

* One column: no handles and the slider was disabled.
* Two columns: one handle.
* Three columns: two handles.
* Four columns: three handles.

Handles snapped to Bootstrap's 12 grid steps.

This made it impossible through the normal interface to create a column configuration that did not correspond to the Bootstrap grid.

### Trade-offs

The new interface was less flexible than the old free-form fields.

That was intentional.

The old flexibility had allowed years of inconsistent and technically invalid configurations to accumulate.

The new system traded arbitrary control for predictable, valid responsive layouts.

---

## Decision: Use Progressive Customer Rollout

### Context

Enabling the new frontend for approximately 1,300 schools simultaneously would have increased the operational risk and could have created a large spike in customer support requests.

### Chosen Solution

The new design was:

1. Developed internally.
2. Tested on local servers.
3. Tested on remote test servers.
4. Tested with pilot customers.
5. Gradually enabled for additional schools.
6. Supported alongside the old frontend during the transition.
7. Eventually enabled for the full customer base.

### Trade-offs

Maintaining both frontends temporarily increased the maintenance burden.

However, it reduced the blast radius of problems and allowed the team to use real customer feedback throughout the rollout.

---

# Implementation

The implementation covered the frontend architecture, PHP rendering layer, page scaffolding, more than 30 CMS modules, responsive behavior, legacy browser compatibility, customer configuration, migration logic, and rollout.

Key implementation work included:

* Rebuilding the page scaffolding around Bootstrap 3.
* Converting more than 30 CMS modules to responsive layouts.
* Refactoring PHP rendering code toward a more object-oriented structure.
* Separating shared application logic from frontend-specific presentation.
* Supporting simultaneous legacy and responsive rendering modes.
* Adding the per-school frontend-mode database setting.
* Restricting the frontend switch to authorized Moava employees.
* Implementing responsive navigation.
* Implementing mobile navigation using modal-style interaction.
* Supporting the browser back button for mobile navigation.
* Making columns stack appropriately on smaller screens.
* Implementing configurable mobile column priority.
* Using the middle/main column as the default mobile priority.
* Making images responsive.
* Adapting tables for smaller screens.
* Adjusting typography for responsive layouts.
* Updating headers and banners.
* Improving touch interaction.
* Adding legacy CSS fallbacks for older browsers.
* Supporting Internet Explorer and other major browsers of the period.
* Analyzing approximately 1,000 existing customer configurations.
* Identifying six recurring legacy column-width patterns.
* Implementing an algorithm for converting legacy values into Bootstrap grid widths.
* Designing a multi-handle custom grid slider for administrators.
* Dynamically changing the number of slider handles according to the number of columns.
* Snapping slider handles to Bootstrap's 12 grid steps.
* Preventing new invalid column configurations through the administration interface.
* Testing on local development servers.
* Deploying to remote test servers.
* Testing with pilot customers.
* Rolling the new frontend out progressively.
* Continuing limited legacy frontend bug fixes during the transition.
* Supporting customer configuration through free design consultations.

---

# Result

The project transformed Moava's customer-facing CMS from a fixed-width, desktop-oriented system into a responsive Bootstrap 3-based platform.

The main outcomes were:

* Approximately 1,300 schools were transitioned to the responsive frontend.
* More than 30 CMS modules were modernized.
* Approximately 39,000 generated pages were covered by the responsive architecture based on the conservative baseline of 30 pages per school.
* The actual customer content was not individually migrated or rebuilt.
* Existing customer databases and content remained usable.
* The old and new frontend modes could operate simultaneously.
* Individual schools could be switched between frontend modes.
* Individual schools could be reverted without a content migration.
* The frontend could be rolled out progressively.
* Pilot customers could test the system before broader rollout.
* Responsive navigation was introduced.
* Mobile-specific navigation and interaction patterns were implemented.
* Columns could stack vertically on mobile.
* Customers could control mobile column priority.
* Images became responsive.
* Tables were adapted for smaller screens.
* Typography and module layouts were adapted for different viewport sizes.
* Headers and banners were redesigned for the new frontend.
* Touch-friendly interactions were introduced.
* Older browsers, particularly Internet Explorer, remained supported through compatibility work and legacy CSS fallbacks.
* Approximately 1,000 existing customer column configurations were analyzed.
* Six recurring patterns were identified in the legacy column-width data.
* An automated algorithm converted inconsistent legacy configurations into Bootstrap-compatible grid layouts.
* The free-form column configuration interface was replaced with a constrained multi-handle custom grid slider.
* Slider handles snapped to one of Bootstraps 12 valid grid positions.
* New invalid column configurations could therefore be prevented through the normal administration interface.
* The same underlying content could be used by both frontend versions.
* Only a small number of customers initially requested to revert to the old design.
* After configuration assistance and discussion with the team, those customers also chose to keep the new version.
* Each customer could receive one hour of free telephone design consultation with a designer.
* Some customers used the consultation to fine-tune column widths or request changes such as a new banner.
* Approximately three months after the new frontend was introduced, all schools had transitioned to the new design.

The project allowed Moava to modernize a large legacy SaaS CMS without forcing approximately 1,300 independent school customers to manually rebuild their websites.

---

# Lessons Learned

## Technical Lessons

A large CMS frontend modernization is not primarily a CSS problem.

When thousands of pages are dynamically generated from reusable modules, the correct migration boundary is the rendering architecture and the reusable components that produce those pages.

Modernizing the scaffolding and modules allowed a very large number of generated pages to become responsive without individually rebuilding them.

The project also demonstrated that legacy configuration data must be treated as real production data.

Values that are technically invalid may still have worked for years because of browser behavior, CSS quirks, or implementation details.

The six legacy column patterns were discovered by analyzing real customer configurations rather than assuming that the existing data followed a clean model.

## Architectural Lessons

Keeping customer content independent from presentation made the migration significantly safer.

The same underlying content could be rendered by either frontend mode.

The per-school frontend switch effectively acted as a feature flag and made progressive rollout and rollback possible.

Refactoring shared PHP logic toward a more object-oriented structure also made it easier to distinguish application behavior from presentation concerns.

## Responsive Design Lessons

Responsive design is not simply a matter of making a desktop layout narrower.

Each module needs to be considered individually.

Navigation, tables, images, forms, calendars, slideshows, typography, headers, banners, and content blocks can all require different responsive behavior.

Mobile information hierarchy also matters.

The desktop position of a column does not necessarily represent its importance on a mobile device, which is why configurable column priority was useful.

## Configuration Design Lessons

Free-form configuration can appear convenient but creates technical debt over time.

The old column system gave customers flexibility, but that flexibility allowed technically invalid combinations to accumulate.

The new slider-based configuration sacrificed arbitrary values in exchange for a predictable and constrained model.

The multi-handle custom grid slider became both a technical constraint and a user-facing configuration model.

## Browser Compatibility Lessons

Modern responsive behavior had to be introduced into an environment where Internet Explorer remained important.

This required compatibility work and legacy CSS fallbacks rather than assuming that all users had modern browsers.

The project demonstrated the practical difficulty of modernizing a legacy frontend while still supporting older environments.

## Process Lessons

Progressive rollout was significantly safer than a big-bang migration.

Pilot customers exposed problems that internal testing alone would not necessarily have revealed.

The ability to enable or disable the new frontend for individual schools meant that problems could be isolated without affecting the entire customer base.

Customer support also became part of the migration process.

The one-hour design consultations gave customers a way to adapt their existing configurations instead of treating the frontend migration as purely a technical deployment.

## Product Lessons

The project was not purely technical.

Mobile access had become an important part of how students interacted with school information.

The modernization therefore addressed a real change in user behavior rather than simply replacing an old visual design.

The project also supported broader business goals:

* Customer retention.
* Customer satisfaction.
* Product modernization.
* Competitiveness.
* Mobile usability.
* Accessibility.
* Universal design.

---

# What I Would Do Differently Today

With modern tooling, I would keep the core architectural strategy but make the migration more observable, testable, and automated.

I would add:

* Automated visual regression testing for the CMS modules.
* Component-level integration tests.
* Automated responsive testing across a defined viewport matrix.
* Automated browser compatibility testing.
* Automated accessibility testing against WCAG requirements.
* Better separation between CMS content, application logic, and presentation.

I would want to know exactly which modules generated the most problems, and which browsers caused compatibility issues.

---

# Interview Notes

## Possible Questions

### What was the actual scope of this project?

It was a full frontend modernization of a SaaS CMS used by approximately 1,300 schools in Norway. The CMS dynamically generated public school websites from customer content and configuration. More than 30 reusable modules contributed to those pages.

I was the product owner and primary developer for the frontend modernization. I worked through all of the 30+ modules and the surrounding frontend architecture, while collaborating with the other developers on the team when needed.

The goal was to move the platform from a fixed-width desktop-oriented frontend to a responsive Bootstrap 3 system without requiring customers to rebuild their websites.

### How large was the system you were modernizing?

Approximately 1,300 schools used the CMS, and each school typically had at least 30 generated pages, giving a conservative baseline of around 39,000 dynamically generated pages.

The important point is that we did not migrate those pages individually. The pages were generated by the CMS, so I modernized the scaffolding and reusable modules that generated them.

### Why couldn't you simply make the existing website responsive with CSS?

The legacy frontend was not built around responsive principles.

It relied on fixed-width layouts, tables for layout, pixel dimensions, custom CSS, inline styling, and module-specific assumptions.

More than 30 modules generated different HTML structures, so changing the outer CSS container would not have been enough.

The modules themselves had to be redesigned to work within a responsive Bootstrap grid.

### How did you migrate tens of thousands of pages without rebuilding them?

We did not migrate the individual pages.

The pages were dynamically generated by the CMS.

I modernized the page scaffolding and the reusable CMS modules that generated those pages.

The underlying customer content stayed the same, so the same content could automatically be rendered through the new responsive frontend.

### How did you support the old and new designs simultaneously?

I implemented a per-school frontend-mode setting stored in each school's database.

PHP checked that setting when generating the public website and selected either the legacy or responsive rendering mode.

That meant one school could use the old frontend while another school used the new Bootstrap 3 frontend.

### How did you make the new design reversible?

The frontend mode was controlled by a database setting accessible through a restricted administration interface available only to Moava employees.

If a school wanted to return to the old design, we could simply switch the setting back.

No content migration or page rebuild was required.

### What was your personal contribution?

I was a new developer on the team, and I became the product owner and primary developer for the modernization project.

I worked through all of the more than 30 CMS modules involved in the frontend, as well as the page scaffolding, PHP rendering architecture, responsive behavior, Bootstrap integration, browser compatibility, column migration logic, and rollout mechanism.

I worked closely with the other developers and asked for their help and input when needed, but I was responsible for driving the implementation across the project.

### What was the hardest technical problem?

One of the hardest problems was converting the existing customer column configurations into Bootstrap's 12-column grid.

The old CMS allowed customers to enter arbitrary values such as:

* 30%
* 20% / 1024 / 20%
* 100px / 400px / 400px / 100px
* 30% / 800px / 30%

Some configurations were technically invalid but had worked because of browser and CSS behavior.

I analyzed approximately 1,000 customer configurations and identified six recurring patterns.

I then built an algorithm that interpreted those patterns and converted them into Bootstrap-compatible grid widths.

### Why did you need an algorithm for column widths?

Because there was no reliable one-to-one conversion.

The old system allowed percentages, pixel values, mixed units, incomplete configurations, and values that did not necessarily add up correctly.

I needed to infer the customer's intended layout from the existing values and convert that into the much more structured Bootstrap 12-column grid.

### What did you do to prevent the problem from happening again?

I redesigned the column configuration interface.

Instead of free-form text fields, the new interface used a multi-handle custom grid slider representing the Bootstrap 12-column grid.

The number of handles changed dynamically according to the number of columns.

With two columns there was one handle, with three columns there were two handles, and with four columns there were three handles.

The handles snapped to the 12 Bootstrap grid steps.

With one column, the slider was disabled because there was no division to configure.

This meant administrators could configure the layout visually while remaining inside the constraints of the Bootstrap grid.

### How did you handle mobile column ordering?

Columns stacked vertically on smaller screens.

We also allowed customers to define column priority so they could control which content appeared first on mobile.

The middle/main column was the default priority because it generally contained the primary content, but customers could change the priority when their particular layout required it.

### What did responsive design mean beyond stacking columns?

We changed much more than the column layout.

The project included:

* Responsive navigation.
* Mobile navigation.
* Modal-style mobile interactions.
* Browser back-button support for mobile navigation.
* Responsive images.
* Responsive tables.
* Responsive typography.
* Responsive forms.
* Responsive module layouts.
* Responsive headers and banners.
* Touch-friendly controls.
* Mobile column prioritization.

### How did you handle older browsers?

Internet Explorer was particularly important because of the school environments using the system.

We also supported the major browsers of the period, including Firefox, Opera, Safari, and Chrome.

Some modules required legacy CSS fallbacks because newer responsive behavior could not be relied upon consistently across older browsers.

### How did you test the new system?

We tested locally and on remote test servers before deploying to production.

We also worked with pilot customers who used the new design in real-world environments.

After the pilot phase, we rolled the design out gradually instead of enabling it for all schools simultaneously.

### Why did you use pilot customers?

Internal testing cannot reproduce every combination of customer content and configuration.

The pilot customers gave us real-world feedback from actual school websites.

That allowed us to find issues before the new design was rolled out to the wider customer base.

### Why did the old and new systems need to coexist?

Because we had approximately 1,300 customers and could not reasonably assume that everything would work perfectly for every school on the first day.

The ability to run both versions allowed us to migrate progressively.

One school could use the old design while another used the new design.

If a problem occurred, we could revert an individual school rather than rolling back the entire platform.

### How long did both systems run?

The old frontend remained available while the new system was rolled out.

We continued making important bug fixes to the old frontend for a period after the new design was introduced, while most development effort shifted to the new system.

Approximately three months after the new frontend was introduced, all schools had been converted to the new design.

### Did customers control the switch themselves?

No.

The switch was available only to authorized Moava employees through the administration interface.

The team enabled the new design for each school as part of the rollout.

If a customer wanted to revert, they contacted us and we could switch them back.

### Did customers have to migrate their content?

No.

The underlying database content remained the same.

The goal was specifically to make the existing content work with both frontend versions.

A few customers wanted to fine-tune column widths or make visual changes such as requesting a new banner, but those were configuration and design adjustments rather than content migrations.

### How did you help customers adapt to the new design?

Moava offered each customer one hour of free telephone design consultation with a designer.

This allowed customers to get help adjusting their configuration and adapting their existing layouts to the new design.

Only a small number of customers initially wanted to revert to the old design, and after discussions and configuration assistance, they also chose to remain on the new version.

### Why did Moava undertake the modernization?

Mobile phones had become an important way for students to access school information such as schedules and weekly plans.

The existing desktop-oriented frontend was increasingly unsuitable for that behavior.

The modernization also supported customer retention, customer satisfaction, product modernization, competitiveness, usability, and the move toward universal-design and accessibility requirements.

### What was the most important architectural decision?

Keeping customer data independent from frontend presentation was one of the most important decisions.

The same underlying content could be rendered through either the legacy or responsive frontend.

That made progressive rollout and rollback possible without requiring customers to migrate or recreate their content.

### What was the biggest lesson from the project?

A large-scale frontend modernization should happen at the reusable rendering layer rather than at the individual page level.

If thousands of pages are generated dynamically from reusable CMS modules, modernizing the components and scaffolding that generate those pages can transform the entire platform without manually rebuilding every page.

---

# Key Talking Points

* Modernized a SaaS CMS used by approximately 1,300 Norwegian schools.
* Took primary responsibility and acted as product owner for the frontend modernization.
* Worked through all 30+ CMS modules involved in the project.
* Collaborated with the development team while driving the implementation.
* Transformed a fixed-width, desktop-oriented frontend into a responsive Bootstrap 3 system.
* Modernized the page scaffolding and reusable rendering components rather than individual pages.
* Covered an estimated baseline of approximately 39,000 dynamically generated pages.
* Kept the underlying customer content unchanged.
* Allowed old and new frontend modes to operate simultaneously.
* Implemented a per-school frontend switch stored in the school's database.
* Restricted the frontend switch to authorized Moava employees.
* Made individual customer rollback possible without data migration.
* Used pilot customers before broader rollout.
* Used progressive rollout to reduce operational risk.
* Modernized more than 30 CMS modules.
* Implemented responsive navigation.
* Implemented mobile-specific navigation and touch interactions.
* Supported browser back-button behavior for mobile navigation.
* Added responsive images, tables, typography, forms, headers, banners, and module layouts.
* Added configurable mobile column priority.
* Used the middle/main column as the default mobile priority.
* Supported older browsers, particularly Internet Explorer.
* Added legacy CSS fallbacks where necessary.
* Analyzed approximately 1,000 existing customer column configurations.
* Identified six recurring patterns in inconsistent legacy layout data.
* Built an algorithm to convert legacy percentages, pixel values, and mixed configurations into Bootstrap's 12-column grid.
* Replaced free-form column configuration with a multi-handle custom grid slider.
* Dynamically used one to three slider handles depending on the number of columns.
* Made the slider inactive for single-column layouts.
* Made slider handles snap to the 12 Bootstrap grid steps.
* Prevented new invalid column configurations through the redesigned interface.
* Used the same customer content across both frontend modes.
* Provided a reversible rollout mechanism.
* Offered customers one hour of free design consultation.
* Helped customers fine-tune column widths and other visual settings.
* Completed the transition of all schools approximately three months after the new frontend was introduced.
* Connected the technical modernization to mobile usage, customer retention, customer satisfaction, competitiveness, usability, accessibility, and universal design.

---

# Future Improvements

If continuing the project today, I would preserve the core architectural approach but introduce significantly stronger automated validation, observability, and testing.

Potential improvements include:

* Automated visual regression testing for the CMS modules.
* Component-level integration tests.
* Automated responsive testing across a defined viewport matrix.
* Automated browser compatibility testing.
* Automated accessibility testing against WCAG requirements.
* Monitoring of module rendering failures.
* Automated detection of responsive layout regressions.
* Stronger separation between content, application logic, and presentation.

---