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
  - bootstrap
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

As part of the modernization of Moava's CMS, I led the frontend transformation from a fixed-width, desktop-oriented design into a responsive Bootstrap 3-based system used by approximately 1,300 schools in Norway.

The existing CMS had been developed over many years and generated public school websites using a pre-2000-style layout approach. Pages relied heavily on fixed widths, tables for layout, pixel-based sizing, custom CSS, inline styling, and a fixed central content area. The system had relatively few global design rules and had not been designed with modern mobile usability, accessibility, or responsive behavior in mind.

The challenge was significantly larger than redesigning a single website. Each school could use more than 30 different CMS modules, including navigation, news, articles, article lists, images, video, slideshows, employee listings, email lists, forms, tables, search, and other content modules. A typical school had at least 30 CMS-generated pages, resulting in an estimated 39,000 or more generated pages across approximately 1,300 schools.

I personally modernized all of the 30+ modules so that existing CMS content could be rendered through a responsive Bootstrap-based frontend without requiring schools to rebuild their websites or migrate their content manually.

The new system needed to coexist with the existing design. A school could use either the old or new frontend while the underlying CMS content and database remained unchanged. I implemented a per-school database switch that controlled which rendering mode PHP used. This allowed the team to gradually introduce the new design, keep the old system available, and immediately revert a school to the previous design if necessary.

The modernization introduced responsive navigation, stacking and prioritization of columns, scalable images, responsive tables, mobile-specific module behavior, improved typography, redesigned headers and banners, and touch-friendly interaction patterns.

The project transformed a large legacy CMS and its customer-facing websites into a responsive platform capable of serving the same underlying content across desktop and mobile devices while allowing the migration to happen incrementally and with minimal customer disruption.

---

# Context

Moava AS operated a custom CMS used by approximately 1,300 schools in Norway to create and manage their public websites.

The CMS generated the public websites dynamically from customer-specific content and configuration stored in each school's database. Customers could combine more than 30 different modules when building their pages, including:

- Navigation and menus.
- News.
- Articles.
- Article lists.
- Images and image galleries.
- Video.
- Slideshows.
- Employees.
- Email lists.
- Forms.
- Tables.
- Calendars.
- Search.
- Content blocks.
- Other school-specific content modules.

The existing frontend had accumulated substantial technical debt over many years. Its layout approach was based on fixed-width desktop websites, tables for layout, fixed pixel dimensions, custom CSS, inline styles, and a fixed central content area. The design had few consistent global rules and used older visual conventions such as hard corners and desktop-first interaction patterns.

This became increasingly problematic as mobile phones became an important way for students to access school information such as schedules and weekly plans. The websites also needed to work well for teachers, parents, students, and other visitors using different devices and browsers.

The modernization was therefore driven by several factors:

- Increasing mobile usage.
- Customer satisfaction and retention.
- Modernizing the CMS and its customer-facing websites.
- Remaining competitive in the market.
- Improving the usability of school websites.
- Supporting the move toward universal design and making content more accessible to people with disabilities.
- Reducing the limitations imposed by the legacy frontend architecture.

The scale of the problem made a page-by-page migration impractical. With approximately 1,300 schools and at least 30 generated pages per school as a rough baseline, the system represented an estimated 39,000 or more dynamically generated pages.

The solution therefore had to modernize the scaffolding and the reusable CMS modules rather than individually rebuilding customer pages.

---

# Task

I joined the development team as a new developer and took primary responsibility for the implementation of the responsive frontend modernization.

My main responsibility was to transform the existing CMS-generated frontend into a responsive Bootstrap 3-based system while preserving compatibility with the existing customer content and the legacy frontend.

I personally worked through all of the 30+ CMS modules, with limited assistance from the three other developers on the team.

My work included:

- Reworking the frontend architecture around Bootstrap 3.
- Converting the existing page scaffolding to support responsive layouts.
- Updating all major CMS modules to produce Bootstrap-compatible HTML and CSS.
- Introducing responsive behavior for navigation, columns, images, tables, forms, and other modules.
- Implementing mobile-specific interaction patterns.
- Designing a mechanism that allowed the old and new rendering systems to coexist.
- Refactoring existing PHP scripts toward a more object-oriented structure so shared application logic could be reused by both rendering modes.
- Implementing the per-school database switch controlling the active frontend mode.
- Handling inconsistent legacy column-width configurations.
- Designing an algorithm to map historical customer configurations to the Bootstrap 12-column grid.
- Maintaining compatibility with older browsers, particularly Internet Explorer.
- Creating legacy CSS fallbacks where newer responsive behavior was not sufficiently supported.
- Testing the new system locally and on remote test servers.
- Working with pilot customers before broader rollout.
- Supporting the controlled rollout and subsequent customer transition.
- Helping customers fine-tune their new layouts when required.

The key requirement was that existing customer content and configuration had to remain usable without requiring customers to rebuild their websites.

---

# Challenge

## Challenge: Modernizing a Legacy CMS Without Rebuilding Customer Websites

### Problem

The CMS was not a single website. It was a multi-tenant platform generating thousands of public school websites from shared application logic and customer-specific content.

The existing frontend had been designed primarily for desktop computers and relied on fixed-width layouts, tables, pixel-based dimensions, custom CSS, inline styles, and assumptions that did not translate well to mobile devices.

A conventional redesign would have required rebuilding or manually adjusting a very large number of existing pages.

That was not practical.

The new system therefore had to modernize the reusable page scaffolding and more than 30 CMS modules while preserving the existing customer content.

At the same time, the old and new designs had to operate simultaneously. One school could use the legacy frontend while another school used the new responsive frontend, with both systems reading the same underlying customer data.

The main constraints were:

- Approximately 1,300 schools were using the CMS.
- Each school could use more than 30 different content modules.
- A typical school had at least 30 generated pages.
- Existing content had to work in both rendering modes.
- Customers should not have to rebuild their pages.
- The old and new designs needed to coexist during the transition.
- The new design had to be reversible.
- The new frontend needed to work across desktop and mobile devices.
- Older browsers, particularly Internet Explorer, still had to be supported.
- The rollout needed to be controlled so problems could be detected before affecting large numbers of customers.

### Solution

I treated the CMS modules and page scaffolding as the migration boundary rather than treating individual customer pages as migration targets.

The existing content and database structures remained unchanged. Instead, the PHP rendering layer was reworked so that the same underlying content could be rendered through either the legacy or responsive frontend.

I refactored the existing PHP scripts toward a more object-oriented design, allowing shared logic to be separated from presentation concerns and reused by the two rendering modes.

A per-school database variable determined which frontend mode was active. PHP checked this value when generating the website and selected the appropriate HTML, CSS, and JavaScript output.

This created a controlled architecture where:

```text
Customer content
       |
       v
     CMS data
       |
       v
 PHP application logic
       |
       +-------------------+
       |                   |
   Legacy mode       Responsive mode
       |                   |
       v                   v
 Legacy HTML/CSS      Bootstrap 3 HTML/CSS/JS
```

This meant the same customer content could continue to drive both versions without requiring a separate content migration.

I then systematically converted the 30+ CMS modules and the surrounding page scaffolding to support responsive behavior.

### Result

The platform could serve the same customer content through either the old or new frontend.

A school could remain on the legacy design while another school used the responsive design, allowing the team to migrate customers progressively instead of performing a single high-risk platform-wide switch.

The new architecture also made it possible to revert an individual school simply by changing its frontend-mode setting. No customer data migration or page rebuild was required.

This provided a practical way to modernize a large legacy CMS without forcing all customers onto the new design simultaneously.

---

## Challenge: Converting Legacy Layout Configurations to the Bootstrap Grid

### Problem

The existing CMS allowed customers to configure between one and four columns using free-form text input fields.

Over the years, customers had entered many different kinds of values. Examples included:

```text
30%
30% / 30%
20% / 1024 / 20%
100px / 400px / 400px / 100px
30% / 800px / 30%
```

These configurations were not necessarily mathematically correct or consistent. Some did not add up to 100%, some mixed percentages and pixel values, and some relied on browser behavior or quirks that happened to make the resulting layout appear acceptable.

The new Bootstrap grid required a much more structured model based on 12 grid columns.

A simple one-to-one conversion was therefore impossible.

The migration had to preserve the approximate visual proportions of existing customer layouts while translating inconsistent historical values into valid Bootstrap-compatible widths.

### Solution

I analyzed the existing customer configuration data and identified six recurring patterns in how column widths had been entered.

I then designed and implemented an algorithm that interpreted the legacy values based on their structure and numeric values rather than assuming they were valid CSS.

The algorithm considered factors such as:

- Whether a value was expressed as a percentage.
- Whether a value was expressed in pixels.
- The number of configured columns.
- The relationship between multiple column values.
- Common recurring combinations in the existing customer data.

The resulting configuration was mapped to the Bootstrap 12-column grid so that the new layout would remain as close as reasonably possible to the previous visual structure.

Where a legacy configuration could not be reliably mapped, standard Bootstrap widths were used based on the number of columns.

The goal was not to reproduce invalid legacy CSS literally. The goal was to preserve the customer's intended layout while converting it into a predictable responsive structure.

### Result

The algorithm allowed inconsistent historical column configurations to be converted automatically rather than requiring developers to manually correct every customer's layout.

Existing customer layouts could therefore transition to Bootstrap while remaining approximately visually consistent.

The new layouts were constrained by the Bootstrap grid, making them more predictable and suitable for responsive behavior.

The approach also prevented the migration from becoming dependent on the accidental browser behavior that had allowed some incorrect legacy configurations to appear functional.

---

## Challenge: Supporting Responsive Behavior Across 30+ CMS Modules

### Problem

The CMS contained more than 30 reusable modules, each of which could appear in different page configurations.

Simply changing the outer page container was not enough. Individual modules had their own HTML structures, CSS assumptions, dimensions, and interaction patterns.

Modules such as navigation, articles, images, slideshows, forms, tables, calendars, search, and content blocks all needed to behave appropriately across different screen sizes.

The new system also needed to support older desktop browsers used in school environments, particularly Internet Explorer.

### Solution

I systematically modified the CMS modules so that their generated HTML, CSS, and JavaScript worked within the Bootstrap responsive layout.

The responsive implementation included:

- Collapsing navigation for smaller screens.
- Mobile navigation using modal-style interaction.
- Supporting the browser back button when closing mobile navigation.
- Stacking columns vertically on smaller screens.
- Allowing customers to define column priority.
- Using the middle/main column as the default priority.
- Allowing customers to change the priority when their content structure required it.
- Scaling images appropriately.
- Making tables usable on smaller screens.
- Adjusting typography for different screen sizes.
- Changing module layouts according to available screen width.
- Adapting headers and banners.
- Improving touch interaction.
- Adding legacy CSS fallbacks for older browser behavior.

### Result

The same CMS-generated content could be presented as a desktop or mobile-friendly website without requiring customers to recreate their pages.

The individual modules became responsive components of a broader Bootstrap-based frontend rather than isolated fixed-width elements.

This allowed the responsive behavior to propagate across the large number of pages generated by the CMS.

---

## Challenge: Introducing the New Frontend Without a High-Risk Big-Bang Migration

### Problem

Migrating approximately 1,300 schools simultaneously would have created a large operational risk.

If a problem affected the new frontend, it could potentially generate a large number of customer support requests at once.

At the same time, customers needed a way to reject or revert the new design if it did not meet their expectations.

### Solution

I implemented a per-school frontend-mode switch stored in each school's database.

The setting was exposed through a design-related area of the CMS administration interface, but access was restricted to Moava employees rather than customer administrators.

This allowed the development team to enable or disable the responsive design independently for each school.

Pilot customers received the new design first. Their feedback was used to identify problems before the broader rollout.

The rollout was then performed gradually rather than enabling the new design for all customers simultaneously.

If a customer wanted to revert to the old design, the team could simply switch the setting back. No database migration or content conversion was required.

The team also continued to fix issues in the old design for a period after the new design launched, while most development effort shifted to the new frontend.

Approximately six months after implementation, all schools had been transitioned to the new design.

### Result

The feature-switch architecture reduced the risk of the migration and provided a practical rollback mechanism.

The team could:

- Enable the new design for individual schools.
- Keep other schools on the old design.
- Test the new design with real customers.
- Roll back individual schools without data migration.
- Gradually increase adoption.
- Avoid a single high-risk platform-wide release.

Only a small number of customers ultimately asked to revert. After discussion with the team and assistance with their settings, they also chose to remain on the new design.

To support the transition further, Moava offered each customer one hour of free telephone design consultation with a designer to help with configuration and visual adjustments. Some customers used this assistance to fine-tune column widths or request a new banner.

---

# Action

## Architecture

### Frontend

The frontend architecture was redesigned around Bootstrap 3 and responsive layout principles.

The existing fixed-width page scaffolding was replaced with a more structured responsive grid, while the individual CMS modules were updated to work within the new layout system.

The frontend supported:

- Bootstrap 3 grid layouts.
- Responsive navigation.
- Mobile-specific interaction patterns.
- Responsive columns.
- Customer-configurable column priority.
- Responsive images.
- Responsive tables.
- Responsive typography.
- Responsive module layouts.
- Responsive headers and banners.
- Touch-friendly interactions.
- Legacy CSS fallbacks for older browsers.

The same CMS content could be rendered through either the legacy or responsive frontend.

### Backend

The application was primarily PHP-based.

I refactored existing PHP scripts toward a more object-oriented structure so that application logic could be separated from frontend presentation and shared by both rendering modes.

The PHP layer checked the per-school frontend-mode setting and selected the appropriate rendering behavior.

This allowed the old and new frontends to coexist within the same application rather than requiring two completely separate CMS applications.

### Database

Each school had its own database containing its CMS content and configuration.

The modernization did not require migrating or duplicating the underlying customer content.

A database variable stored the frontend-mode setting for each school.

This setting determined whether PHP generated the legacy or responsive version of the frontend.

The same customer content therefore remained available to both rendering modes.

### Infrastructure

Development and testing were performed using local development servers and remote test servers.

Code could be deployed to remote test environments before being committed and pushed to the live environment.

Pilot customers were also used as part of the validation process before broader rollout.

The production rollout was performed progressively to reduce the risk of large-scale issues and customer support spikes.

---

## Technical Decisions

## Decision: Use Bootstrap 3 as the Responsive Layout Foundation

### Context

The existing frontend relied on fixed-width layouts, tables, pixel-based sizing, custom CSS, and other legacy techniques that did not provide a practical foundation for modern mobile layouts.

The CMS needed a consistent responsive layout system that could be applied across many different modules and customer websites.

### Chosen Solution

Bootstrap 3 was selected as the foundation for the new responsive frontend.

The existing page scaffolding and 30+ CMS modules were adapted to use Bootstrap's grid and responsive conventions.

### Alternatives Considered

The available information does not establish specific alternative frameworks that were formally evaluated.

A custom responsive CSS system would have been another possible approach, but the project required a consistent grid and responsive behavior across a large number of reusable modules.

### Trade-offs

Using Bootstrap introduced a structured grid and common responsive conventions, which significantly simplified the task of making a large number of modules behave consistently across screen sizes.

The trade-off was that existing arbitrary customer configurations had to be normalized to fit the Bootstrap model.

The project therefore required migration logic rather than simply replacing the existing CSS.

---

## Decision: Keep the Old and New Frontends Running Simultaneously

### Context

A platform-wide replacement would have created unnecessary operational and customer risk.

Existing customer content also needed to work without requiring customers to rebuild their websites.

### Chosen Solution

The old and new frontend rendering modes were maintained simultaneously.

A per-school database setting determined which rendering mode PHP used.

Shared application logic was refactored toward a more object-oriented structure so that the two presentation modes could reuse the same underlying content and logic.

### Alternatives Considered

Potential alternatives included:

- Requiring all schools to migrate simultaneously.
- Creating a completely separate CMS for the new frontend.
- Requiring customers to rebuild their pages before switching.
- Maintaining separate copies of customer content for each frontend.

These approaches would have increased migration complexity and operational risk.

### Trade-offs

Maintaining two frontend modes temporarily increased the amount of code that had to be supported.

The team also continued to fix bugs in the legacy frontend for a period after the new design launched.

However, the approach provided important benefits:

- Individual customer rollout.
- Immediate rollback.
- Shared customer content.
- No page-by-page migration.
- Safer pilot testing.
- Reduced risk during the transition.

The legacy frontend was eventually retired after the schools had been migrated to the new system.

---

## Decision: Normalize Legacy Column Configurations Instead of Preserving Invalid CSS

### Context

Customers had historically been allowed to enter column widths using free-form values.

These values could contain percentages, pixel values, incomplete percentages, mixed units, and other configurations that happened to work under the legacy browser/CSS environment.

Bootstrap required a more predictable 12-column grid.

### Chosen Solution

The existing configurations were analyzed and grouped into six recurring patterns.

An automated algorithm converted the legacy configurations into Bootstrap-compatible grid widths while attempting to preserve the approximate visual proportions of the original layouts.

### Alternatives Considered

Potential alternatives included:

- Preserving the existing arbitrary CSS values.
- Requiring every customer to manually configure their columns again.
- Using one generic conversion formula.
- Ignoring legacy values and assigning identical Bootstrap widths to every column.

### Trade-offs

The conversion algorithm required additional migration logic and could not perfectly reproduce every unusual legacy configuration.

However, it provided a predictable result and eliminated the need for manual conversion across the large customer base.

The important principle was to preserve the customer's apparent layout intent rather than preserve technically incorrect legacy CSS.

---

## Decision: Use Progressive Customer Rollout

### Context

Enabling a completely new frontend for approximately 1,300 schools at once would have increased the risk of widespread bugs and a large volume of support requests.

### Chosen Solution

The responsive frontend was first tested internally and with pilot customers.

The team then gradually enabled the new design for additional schools using the per-school frontend-mode switch.

Customers who needed help could receive one hour of free telephone design consultation with a Moava designer.

### Alternatives Considered

Potential alternatives included:

- Enabling the new frontend for every school simultaneously.
- Requiring every school to test and approve the design before activation.
- Waiting until all possible issues had been resolved before enabling any customers.

### Trade-offs

Progressive rollout required the team to temporarily maintain both frontend modes.

However, it allowed real-world feedback to be incorporated while limiting the blast radius of problems.

---

## Implementation

The implementation covered the CMS page scaffolding, more than 30 reusable modules, the PHP rendering architecture, legacy browser support, responsive behavior, and the customer rollout mechanism.

Key implementation work included:

- Rebuilding the frontend page scaffolding around Bootstrap 3.
- Converting more than 30 CMS modules to responsive layouts.
- Refactoring PHP rendering code toward a more object-oriented architecture.
- Separating shared application logic from frontend-specific rendering.
- Supporting simultaneous legacy and responsive rendering modes.
- Adding the per-school frontend-mode setting.
- Restricting the design switch to Moava employee accounts.
- Implementing responsive navigation.
- Implementing mobile navigation with modal interaction.
- Supporting the browser back button for mobile navigation behavior.
- Making columns stack appropriately on smaller screens.
- Implementing configurable column priority.
- Using the main/middle column as the default mobile priority.
- Making images responsive.
- Adapting tables for smaller screens.
- Adjusting typography for responsive layouts.
- Updating headers and banners.
- Improving touch interaction.
- Adding legacy CSS fallbacks for older browsers.
- Analyzing approximately 1,000 customer configurations.
- Identifying six recurring legacy column-width patterns.
- Implementing the Bootstrap column conversion algorithm.
- Testing on local development servers.
- Deploying to remote test servers.
- Testing with pilot customers.
- Rolling the new frontend out gradually.
- Continuing limited legacy frontend bug fixes during the transition.
- Supporting customers with design configuration through free telephone consultations.

---

# Result

The project transformed Moava's customer-facing CMS frontend from a fixed-width, desktop-oriented system into a responsive Bootstrap 3-based platform.

The main outcomes were:

- Approximately 1,300 schools were migrated to the new responsive frontend.
- The CMS continued to use the same underlying customer content.
- Customers did not need to rebuild or manually migrate their pages.
- More than 30 CMS modules were modernized for responsive behavior.
- An estimated 39,000 or more generated pages could benefit from the responsive architecture without being individually rebuilt.
- The same content could be rendered through both the legacy and responsive frontend during the transition.
- Individual schools could be switched between frontend modes without a data migration.
- The new design could be rolled out progressively rather than through a high-risk big-bang release.
- A rollback mechanism was available throughout the transition.
- Responsive navigation, columns, images, tables, typography, headers, banners, and module layouts were introduced.
- Customers could configure column priority for mobile layouts.
- Legacy column configurations were automatically converted to the Bootstrap 12-column grid.
- Six recurring patterns in approximately 1,000 customer configurations were identified and handled algorithmically.
- Older browsers, particularly Internet Explorer, remained supported through responsive implementation and legacy CSS fallbacks.
- Pilot customers provided feedback before the broader rollout.
- Approximately six months after implementation, all schools had transitioned to the new design.
- Only a small number of customers initially requested to revert, and after assistance from the team, they also chose to remain on the new version.
- Free design consultations helped customers adjust settings and fine-tune their new layouts.

The project improved the mobile usability and consistency of the platform while allowing Moava to modernize a large legacy CMS without forcing customers through a manual content migration.

No specific quantitative metrics were provided for performance improvements, development time saved, or customer satisfaction, so none are claimed.

---

# Lessons Learned

## Technical Lessons

A frontend modernization of a large CMS is not primarily a CSS replacement problem.

When thousands of customer pages are generated from reusable modules, the real migration boundary is the rendering architecture and the reusable components that produce the pages.

Modernizing the modules and scaffolding allowed a very large number of generated pages to become responsive without individually rebuilding them.

Legacy customer configuration also needs to be treated as real production data. Values that are technically invalid may still have worked for years because of browser behavior, CSS quirks, or application-specific behavior.

The column-width migration demonstrated the importance of analyzing actual production configurations before designing conversion rules.

## Architectural Lessons

Maintaining shared application logic while separating presentation modes made it possible to run old and new frontends simultaneously.

The per-school frontend switch provided an effective feature-flag mechanism long before feature flags became a common architectural pattern.

The same content could be rendered through different presentation layers, which significantly reduced the risk of the migration.

The project also demonstrated that backward compatibility can be a deliberate architectural feature rather than something added after the fact.

## Responsive Design Lessons

Responsive design is more than making a page narrower.

Each reusable module needs to be considered independently.

Navigation, tables, images, columns, typography, headers, forms, and interactive elements can all require different responsive behavior.

For complex CMS layouts, giving customers control over mobile column priority was also important. A desktop layout does not necessarily have the same information hierarchy on a small screen.

## Browser Compatibility Lessons

Introducing modern responsive techniques into a school environment required continued support for older browsers.

This meant that responsive behavior could not simply assume modern browser capabilities. Some modules required legacy CSS fallbacks to maintain acceptable behavior in older Internet Explorer versions and other browsers.

## Process Lessons

A progressive rollout was significantly safer than attempting to migrate all customers simultaneously.

Pilot customers exposed issues that internal testing alone would not necessarily have identified.

The ability to switch individual customers between frontend versions made it possible to address problems without rolling back the entire platform.

Customer support was also part of the technical rollout. Providing design assistance helped customers adapt their existing configurations instead of treating the migration as purely a software deployment.

## What I Would Do Differently Today

With modern tooling, I would strengthen the same architectural approach with:

- Automated visual regression testing across representative CMS modules.
- Automated responsive testing across a defined browser/device matrix.
- Component-level integration tests for every module.
- Formal schema validation for customer layout configuration.
- A structured migration-reporting system.
- Metrics for frontend-mode adoption and rollback frequency.
- Automated detection of unsupported legacy configurations.
- A more formal feature-flag management system.
- Automated accessibility testing against WCAG requirements.
- A documented deprecation lifecycle for the legacy frontend.
- Better observability around module rendering failures.

---

# Interview Notes

## Possible Questions

### What was the actual scope of this project?

It was a full frontend modernization of a CMS used by approximately 1,300 schools in Norway. The CMS generated thousands of public pages from more than 30 reusable modules. I modernized the page scaffolding and all of the major modules so the same underlying customer content could be rendered through a responsive Bootstrap 3 frontend.

### Why couldn't you simply make the existing website responsive with CSS?

The existing frontend was built around fixed-width layouts, tables, pixel dimensions, custom CSS, inline styles, and module-specific assumptions. The problem was distributed across the entire rendering system. More than 30 different modules generated different HTML structures, so making only the outer page responsive would not have solved the problem.

### How did you migrate tens of thousands of pages without rebuilding them?

We didn't migrate the individual pages. The pages were generated dynamically by the CMS. I modernized the page scaffolding and the reusable modules that generated those pages. Because the underlying content remained unchanged, the same content could automatically be rendered through the new responsive frontend.

### How did you support the old and new designs simultaneously?

I introduced a per-school frontend-mode setting stored in the school's database. PHP checked that setting and selected the appropriate rendering mode. One school could therefore use the old frontend while another used the new Bootstrap-based frontend.

### How did you make the new design reversible?

The frontend mode was controlled by a database setting accessible through a restricted administration interface available only to Moava employees. If a school needed to return to the old design, we could switch the setting back. No content migration or database conversion was required.

### Why was it important to keep the old and new systems running simultaneously?

We had approximately 1,300 customers, so a big-bang migration would have created unnecessary operational risk. Running both modes allowed us to test with pilot customers, roll out gradually, and isolate problems to individual schools.

### What was your personal contribution?

I was a new developer on a team of four other developers, but I personally worked through all of the more than 30 CMS modules involved in the frontend modernization. I also worked on the PHP architecture, responsive behavior, Bootstrap integration, legacy browser support, and the automated column-width conversion.

### What was the hardest technical problem?

One of the hardest problems was converting the existing customer layout configurations into Bootstrap's 12-column grid. Customers had historically been able to enter free-form values such as percentages, pixel values, mixed units, and values that did not add up correctly. Those configurations had often worked because of browser and CSS behavior. I analyzed approximately 1,000 customer configurations, identified six recurring patterns, and built an algorithm to map them to Bootstrap-compatible layouts.

### Why did you need an algorithm for column widths?

Because the existing data was inconsistent. A customer might have configured something like `30% / 800px / 30%`, while another might use `100px / 400px / 400px / 100px`. There was no reliable one-to-one conversion. I had to infer the intended layout from the values and convert it into the Bootstrap 12-column model.

### How did you handle mobile column ordering?

The responsive layout allowed customers to define column priority. The middle/main column was the default priority because it generally contained the most important content, but customers could change the priority when their particular page structure required a different order.

### What did responsive design mean beyond stacking columns?

We redesigned navigation, images, tables, typography, headers, banners, module layouts, and touch interactions. Mobile navigation also used a modal-style interaction and supported closing through the browser back button.

### How did you handle older browsers?

Schools still relied heavily on Internet Explorer, so browser compatibility was an important constraint. I implemented legacy CSS fallbacks for modules where newer responsive techniques were not sufficient, while also supporting the major browsers of the time, including Firefox, Opera, Safari, and Chrome.

### How did you test the new system?

We tested on local development servers and remote test servers before deploying to production. We also used pilot customers to test the new design in real-world environments. After the pilot phase, we rolled the design out gradually rather than enabling it for all schools simultaneously.

### Why did Moava need the modernization?

Mobile phones had become an important way for students to access school information such as schedules and weekly plans. The existing desktop-oriented websites were becoming increasingly unsuitable for that use case. The modernization was driven by customer retention, customer satisfaction, product modernization, competitiveness, and the need to make school websites more accessible and suitable for universal-design requirements.

### What was the most important architectural decision?

Keeping the customer data independent from the frontend presentation was one of the most important decisions. The same underlying content could be rendered by either the old or new frontend. That made progressive rollout and rollback possible without requiring customers to migrate or recreate their content.

### What is the biggest lesson from the project?

Large-scale frontend modernization should be performed at the reusable rendering layer rather than at the individual page level. If thousands of pages are generated from a CMS, modernizing the components and scaffolding that generate those pages can transform the entire platform without manually rebuilding every page.

---

## Key Talking Points

- Modernized a custom CMS serving approximately 1,300 Norwegian schools.
- Personally worked through all 30+ CMS modules involved in the frontend modernization.
- Transformed a fixed-width, desktop-oriented frontend into a responsive Bootstrap 3 system.
- Modernized an estimated 39,000+ dynamically generated pages without rebuilding them individually.
- Reworked the page scaffolding and reusable modules rather than migrating individual pages.
- Maintained identical underlying customer content between old and new frontend modes.
- Refactored legacy PHP toward a more object-oriented architecture.
- Separated shared application logic from frontend-specific rendering.
- Implemented a per-school database feature switch for the frontend mode.
- Enabled old and new designs to coexist during the migration.
- Made individual customer rollback possible without data migration.
- Used pilot customers and progressive rollout to reduce operational risk.
- Converted more than 30 modules to responsive Bootstrap-compatible layouts.
- Implemented responsive navigation and mobile-specific interactions.
- Added configurable mobile column priority.
- Supported responsive images, tables, typography, headers, banners, and touch interactions.
- Supported older browsers, particularly Internet Explorer, using legacy CSS fallbacks.
- Analyzed approximately 1,000 customer configurations.
- Identified six recurring patterns in inconsistent legacy column-width data.
- Built an algorithm to convert arbitrary legacy layout values into the Bootstrap 12-column grid.
- Preserved approximate existing layouts rather than forcing customers to rebuild them.
- Supported customers through free one-hour design consultations.
- Completed the transition of all schools to the new design approximately six months after implementation.
- Connected technical modernization directly to mobile usability, customer satisfaction, retention, competitiveness, and accessibility goals.

---

# Future Improvements

If continuing the project today, I would build on the same architecture while adding stronger automated validation, observability, and accessibility tooling.

Potential improvements include:

- Introduce automated visual regression testing for all CMS modules.
- Test every module across a defined set of viewport sizes.
- Automate browser compatibility testing.
- Add automated accessibility testing against WCAG requirements.
- Replace the custom frontend-mode switch with a formal feature-flag system.
- Add rollout dashboards showing which schools use each frontend version.
- Track rollback frequency and migration issues.
- Add structured migration reports for legacy layout configurations.
- Add automated validation for new column configurations so invalid values cannot be introduced again.
- Add automated tests covering all six legacy column-width conversion patterns.
- Log and review configurations that fall outside known migration patterns.
- Add component-level integration tests for the 30+ CMS modules.
- Add a formal deprecation and removal plan for the legacy frontend.
- Introduce stronger separation between CMS content, application logic, and presentation.
- Use modern responsive and accessibility standards to further improve the user experience for students, teachers, parents, and visitors with disabilities.

---