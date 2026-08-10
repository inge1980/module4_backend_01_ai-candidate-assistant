---
title: Automated Bootstrap Migration for Large-Scale CMS

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
  - automation
  - responsive-design
  - scalability
  - cms
  - user-experience
  - technical-debt
  - data-migration
  - algorithm-design
  - progressive-rollout

links:
  github:
  live:

---

# Overview

As part of the modernization of Moava's CMS, I worked on migrating the platform's existing manually configured column layouts from traditional CSS to the Bootstrap framework.

A key problem was that column widths had historically been entered manually by customers, resulting in inconsistent values across approximately 1,000 customer configurations. These values could not be migrated directly to Bootstrap's fixed grid system.

I analyzed the existing customer data, identified six recurring patterns in how column widths had been configured, and developed an automated algorithm to convert the inconsistent values into appropriate Bootstrap widths. Remaining columns were assigned standard Bootstrap widths based on the number of columns in the layout.

The solution made the transition to responsive, mobile-friendly design faster and more predictable while reducing the amount of manual migration work required from developers.

---

# Context

Moava AS operated a CMS used by schools and other customers to create and manage content.

As part of improving the platform's user experience, the frontend was being transitioned from traditional CSS-based layouts to Bootstrap. One of the goals was to provide a more consistent and mobile-friendly experience across the CMS.

The existing platform had accumulated technical debt over time. In particular, column widths in the CMS had historically been entered manually. Because customers had been free to use different values, the stored configurations were not consistent enough to map directly to Bootstrap's grid system.

During testing with pilot customers, these inconsistencies became apparent. Some existing layouts could not be migrated reliably using a simple one-to-one conversion.

The migration therefore needed to:

* Preserve the approximate visual structure of existing customer layouts.
* Convert inconsistent legacy values into valid Bootstrap column widths.
* Handle a large number of existing customer configurations.
* Avoid requiring developers to manually correct each customer configuration.
* Produce predictable layouts suitable for responsive and mobile-friendly design.
* Allow the new responsive implementation to be tested with pilot customers before a broader rollout.

---

# Task

My responsibility was to help modernize the CMS frontend and make the transition to Bootstrap practical for existing customer configurations.

I was specifically responsible for analyzing the existing column-width data and developing an automated migration strategy.

My work included:

* Investigating how approximately 1,000 customers had configured column widths.
* Identifying recurring patterns in the existing data.
* Determining how inconsistent legacy values could be mapped to Bootstrap widths.
* Designing and implementing an algorithm for the conversion.
* Defining fallback behavior for columns that did not match the identified patterns.
* Supporting the transition toward responsive and mobile-friendly layouts.
* Using feedback from pilot customers to validate the migration before broader implementation.

The goal was not simply to replace CSS. The important part of the task was making an existing, inconsistent data set compatible with a more structured frontend layout system without requiring extensive manual intervention.

---

# Challenge

## Challenge: Migrating Inconsistent Legacy Column Configurations to Bootstrap

### Problem

The existing CMS allowed column widths to be entered manually, and customers had used inconsistent values over time.

Bootstrap introduced a more structured grid system with predefined column widths. This created a migration problem: the existing values could not simply be copied into the new system because many of them did not correspond cleanly to valid Bootstrap widths.

The problem became visible during testing with pilot customers. Layouts that appeared reasonable under the previous CSS implementation could produce unexpected results when interpreted using Bootstrap.

The migration had to account for real-world legacy data rather than an idealized data model.

The main constraints were:

* Approximately 1,000 customer configurations had to be considered.
* Existing values were inconsistent.
* Existing layouts should remain as visually similar as reasonably possible.
* The resulting configurations needed to fit within Bootstrap's grid constraints.
* Manual correction of every configuration would be inefficient and error-prone.
* The migration needed to be validated with real customers before full rollout.

### Solution

I analyzed the column-width data from approximately 1,000 customers and identified six distinct patterns in how the legacy values had been configured.

Based on these patterns, I developed an algorithm that:

1. Examined the existing column-width configuration.
2. Classified the configuration according to the identified patterns.
3. Converted the legacy values into corresponding Bootstrap-compatible widths.
4. Preserved the approximate proportions of the original layout where possible.
5. Applied standard Bootstrap widths to remaining columns based on the number of columns when an existing value could not be reliably mapped.

This transformed the migration from a manual configuration exercise into an automated conversion process.

The algorithm was deliberately based on observed production data rather than assumptions about how customers should have configured their layouts.

### Result

The migration could automatically convert the existing customer configurations into Bootstrap-compatible layouts while maintaining an approximation of their previous column structure.

The resulting layouts were constrained to valid Bootstrap widths, which made future configurations more consistent and reduced the risk of the same type of invalid or unexpected values being introduced again.

The automation also reduced the amount of manual migration work required from developers and made the transition to responsive design more manageable.

Pilot customers were used to validate the approach before the broader rollout. Their feedback helped identify problems early and allowed the migration to be refined before full implementation.

---

# Action

## Architecture

### Frontend

The frontend was being transitioned from traditional CSS-based layouts to Bootstrap.

The migration introduced a more structured grid model for column layouts, replacing arbitrary manually entered width values with Bootstrap-compatible column widths.

The work also supported the broader goal of making the CMS responsive and more usable on mobile devices.

Key frontend concerns included:

* Bootstrap grid-based layouts.
* Responsive behavior across screen sizes.
* Preserving the approximate appearance of existing customer layouts.
* Converting legacy column configurations into predictable frontend structures.
* Reducing inconsistencies caused by manually entered layout values.

### Backend

The provided information does not establish a specific backend architecture for this migration.

The migration logic operated on existing customer configuration data and generated Bootstrap-compatible column configurations.

### Database

The migration relied on existing customer data containing manually configured column-width values.

The important database-related challenge was not the introduction of a new storage system, but the inconsistent historical data that had accumulated in the CMS.

Approximately 1,000 customer configurations were analyzed to determine how the legacy values had actually been used.

### Infrastructure

No specific infrastructure, hosting, CI/CD, or deployment architecture is documented for this project.

The rollout strategy did, however, include validation with pilot customers before broader implementation.

---

## Technical Decisions

## Decision: Use Data-Driven Automated Conversion

### Context

The existing customer configurations contained inconsistent manually entered column widths.

A simple fixed conversion rule would not have been reliable because the same type of legacy value could not necessarily be interpreted consistently across all configurations.

Manually correcting approximately 1,000 customer configurations would also have been inefficient and difficult to maintain.

### Chosen Solution

I analyzed the existing data and identified six recurring configuration patterns.

The migration algorithm used these patterns to determine how legacy column widths should be converted into Bootstrap-compatible values.

When an existing configuration could not be mapped reliably, the remaining columns were assigned standard Bootstrap widths based on the number of columns.

### Alternatives Considered

Potential alternatives included:

* Manually correcting customer configurations.
* Applying a single generic conversion formula to all legacy values.
* Requiring customers to manually rebuild their layouts.
* Migrating the CSS without normalizing the underlying column configurations.

These approaches would either have required significantly more manual work or provided less predictable results for existing customer layouts.

### Trade-offs

The data-driven algorithm required analysis of real customer configurations and additional migration logic.

However, this complexity was preferable to treating the legacy data as if it were already consistent.

The approach provided:

* Automated migration.
* More predictable Bootstrap configurations.
* Better preservation of existing layouts.
* Less manual developer work.
* A controlled migration process based on real customer data.

The main limitation was that the algorithm was designed around the patterns present in the existing data. Unusual configurations outside those patterns required fallback behavior rather than a perfect one-to-one conversion.

---

## Implementation

The implementation consisted primarily of analyzing existing CMS configuration data and creating an automated conversion algorithm.

Key implementation work included:

* Analyzing approximately 1,000 customer configurations.
* Identifying six recurring patterns in legacy column-width values.
* Implementing rules for converting those patterns into Bootstrap widths.
* Preserving approximate existing column proportions where possible.
* Applying standard Bootstrap defaults based on the number of columns when required.
* Constraining resulting configurations to the Bootstrap grid model.
* Testing the migration against pilot customer configurations.
* Incorporating pilot customer feedback before broader rollout.

The migration converted a previously inconsistent manual configuration process into a repeatable automated process.

---

# Result

The Bootstrap migration provided a more controlled transition from legacy CSS layouts to responsive Bootstrap-based layouts.

The main outcomes were:

* Approximately 1,000 customer configurations were analyzed to understand the existing data.
* Six recurring legacy configuration patterns were identified.
* An automated algorithm was created to convert inconsistent legacy widths into Bootstrap-compatible widths.
* Existing layouts were preserved approximately rather than being rebuilt from scratch.
* Remaining columns could fall back to standard Bootstrap widths based on the number of columns.
* Manual migration effort for developers was reduced.
* Column configurations became more consistent and constrained by the Bootstrap grid.
* The CMS became better suited for responsive and mobile-friendly layouts.
* Pilot customer feedback was incorporated before the broader rollout, reducing the risk of large-scale migration problems.

No specific quantitative performance, cost, or time-saved metrics were provided, so none are claimed here.

---

# Lessons Learned

## Technical Lessons

Real-world legacy data rarely follows the assumptions made when designing a new system.

The Bootstrap migration demonstrated that replacing a frontend framework can become a data-migration problem when the existing UI configuration is stored as customer data.

Analyzing actual production data before designing the migration rules was therefore critical. The six recurring patterns were discovered from customer configurations rather than assumed in advance.

## Architectural Lessons

Introducing stricter frontend conventions can expose inconsistencies that previously remained hidden.

The legacy system allowed flexible manual values, while Bootstrap required a more constrained grid model. The migration therefore needed an explicit normalization step between the two models.

Automation was preferable to manual cleanup because the same conversion logic could be applied consistently across a large number of customer configurations.

## Process Lessons

Pilot customers provided an important validation step.

Testing the migration with real customer configurations before full rollout made it possible to identify problems that would not necessarily have appeared in controlled development data.

## What I Would Do Differently Today

With modern tooling, I would make the migration more explicit and observable by adding:

* A dry-run migration mode.
* Before/after configuration reports.
* Automated validation of Bootstrap grid constraints.
* Automated tests covering each identified migration pattern.
* Logging of configurations that fall outside known patterns.
* A rollback strategy.
* Migration metrics showing how many configurations matched each rule and how many required fallback handling.

---

# Interview Notes

## Possible Questions

### How did you migrate inconsistent legacy column widths to Bootstrap?

I analyzed approximately 1,000 customer configurations and identified six recurring patterns in how column widths had historically been configured. I then implemented an algorithm that mapped those patterns to Bootstrap-compatible widths while preserving the approximate structure of the existing layouts. Configurations that did not map reliably used standard Bootstrap widths based on the number of columns.

### Why couldn't you simply convert the existing values directly?

The legacy values had been manually entered over time and were inconsistent. Bootstrap uses a more structured grid system, so arbitrary legacy values could not reliably be mapped one-to-one. We first had to understand the actual patterns in the existing customer data.

### Why did you analyze the customer data before implementing the migration?

Because the migration rules needed to reflect how the system had actually been used. Making assumptions about the legacy data would have created incorrect mappings. Analyzing approximately 1,000 customer configurations revealed six recurring patterns that could be handled systematically.

### Why automate the migration instead of fixing the configurations manually?

There were too many customer configurations for manual correction to be an efficient or reliable approach. Automation made the migration repeatable and consistent while reducing developer effort.

### How did you validate the migration?

Pilot customers tested the mobile-friendly version before the broader rollout. Their feedback exposed inconsistencies and helped validate and refine the migration approach before full implementation.

### What was the hardest part of the project?

The difficult part was not introducing Bootstrap itself. It was translating inconsistent real-world legacy configuration data into a stricter grid system while keeping the existing layouts approximately intact.

### What is the main architectural lesson from this project?

A frontend migration can become a data-migration problem when UI configuration is stored as persistent customer data. Before replacing a framework or introducing stricter conventions, the existing data needs to be analyzed and normalized.

---

## Key Talking Points

* Migrated legacy CMS layouts toward Bootstrap and responsive design.
* Worked with approximately 1,000 real customer configurations.
* Discovered six distinct patterns in inconsistent legacy column-width data.
* Designed an algorithm to automate the migration.
* Converted arbitrary legacy values into constrained Bootstrap grid widths.
* Preserved the approximate structure of existing customer layouts.
* Used standard Bootstrap defaults when legacy values could not be reliably mapped.
* Reduced manual migration work for developers.
* Used pilot customers to validate the migration before broader rollout.
* Demonstrated that frontend modernization can require data normalization when UI configuration is persisted.
* Balanced backward compatibility with the need for a more consistent responsive architecture.

---

# Future Improvements

If continuing the project today, I would improve the migration process with stronger validation, observability, and rollback capabilities.

Potential improvements include:

* Add a dry-run mode that reports proposed changes without modifying customer configurations.
* Generate before/after migration reports for each customer.
* Add automated tests for all six identified migration patterns.
* Validate that every generated configuration conforms to Bootstrap's grid constraints.
* Log configurations that do not match known patterns.
* Add migration statistics to measure rule coverage and fallback usage.
* Provide a rollback mechanism for incorrectly migrated configurations.
* Introduce automated visual regression testing for representative customer layouts.
* Gradually migrate customers in controlled batches instead of performing a single large migration.
* Establish stricter validation for newly created column configurations so the legacy inconsistency cannot reappear.

---
