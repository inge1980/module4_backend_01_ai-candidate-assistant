---
title: Hotel Booking Interview Case 2024

organization: 24SevenOffice

role: Frontend Developer

environment: demo

period:
  from: 2024-10
  to: 2024-10

status: completed

technologies:
  - react
  - typescript
  - next.js
  - material-ui
  - emotion
  - dayjs

concepts:
  - form-validation
  - error-handling
  - user-experience

dependencies:
  - "@emotion/react"
  - "@emotion/styled"
  - "@mui/icons-material"
  - "@mui/material"
  - "@mui/x-date-pickers"
  - dayjs
  - next
  - react
  - react-dom

links:
  github: https://github.com/inge1980/hotel_booking_case_2024_improved
  live: https://hotel-booking-case-2024-improved.vercel.app

---

# Overview

A simulated hotel booking application built with React and Next.js, with a particular focus on form validation, error handling, and user experience.

The project explores how a booking interface can provide clear, immediate feedback when users enter invalid or incomplete information, making the booking flow easier to understand and use.

---

# Context

The project was created as a hotel booking case focused on improving the user experience around form input and error handling.

A key requirement was to make validation and feedback clearer while keeping the booking flow straightforward and predictable for the user.

---

# Task

My responsibility was to develop the frontend of the hotel booking application, with particular focus on form validation, error handling, and the overall user experience.

The goal was to create a functional booking flow that clearly communicated validation errors and helped users correct their input before completing the flow.

---

# Challenge

## Challenge: Form Validation and Error Handling

### Problem

A booking flow depends on users providing valid information. When required or invalid input is not handled clearly, users may not understand what needs to be corrected or why they cannot continue.

The challenge was therefore not only to validate the form, but to make validation feedback useful within the actual booking flow.

### Solution

I implemented form validation with a focus on clear and immediate user feedback.

Invalid or missing values are identified during the booking flow, and the interface communicates what needs to be corrected. The error handling is designed to keep the feedback close to the relevant user input rather than relying on unclear or delayed feedback.

The implementation uses React and TypeScript together with Material UI components and MUI X Date Pickers for the form and date-related interactions.

### Result

The booking flow provides clearer feedback when validation fails, making it easier for users to identify and correct invalid or missing information before proceeding.

---

# Action

## Architecture

### Frontend

The application is built with React and Next.js, using TypeScript as the primary development language.

Material UI provides the main UI component library, with Emotion used for styling. MUI X Date Pickers and Day.js are used for date-related input and handling.

The frontend is responsible for the booking interface, user input, validation, and feedback throughout the booking flow.

### Infrastructure

The application is built with Next.js and has a deployed version available as a live demo.

---

## Technical Decisions

### Decision: Material UI for the User Interface

#### Context

The booking interface requires reusable UI components as well as components for form and date interactions.

#### Chosen Solution

Material UI was used as the main UI component library, together with MUI X Date Pickers for date selection.

#### Trade-offs

Using an established component library speeds up development and provides consistent UI patterns, but also introduces dependencies on the library's components and APIs.

---

## Implementation

### Features

- Hotel booking interface
- Form validation
- Automatic feedback for validation errors
- Error handling
- Date selection
- UX-focused booking flow

### Automation

The project includes development and build scripts through the Next.js setup.

---

# Result

The project resulted in a simulated hotel booking application focused on a clear booking flow, form validation, and improved error handling.

The application is deployed as a live demo and can be accessed through the project link in the front matter.

---

# Lessons Learned

## Lesson: Validation Is Part of the User Experience

Form validation is not only about determining whether input is technically valid. How validation errors are communicated is equally important.

This project reinforced the importance of making feedback clear, immediate, and relevant to the user's current interaction with the form.

---