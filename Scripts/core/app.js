
        $(document).ready(function () {

            // Setup initial page parameters.
            Page.setup({
                organizationId: "c08bdab7-ed3d-4048-8338-d4f14f2770a8",
                numberOfColumnsPerRow: 3,
                studentsUrl: "http://localhost:45959/api/students/",
                coursesUrl: "http://localhost:45959/api/courses/",
                //studentsUrl: "http://api.wu15.se/api/students/",
                //coursesUrl: "http://api.wu15.se/api/courses/",
                defaultPlaceholder: $("#defaultPlaceholder"),
                courseDetailsPlaceholder: $("#courseDetailsPlaceholder"),
                courseDetailsStudentListPlaceholder: $("#courseDetailsStudentListPlaceholder"),
                courseDetailsStudentSelectList: $("#courseDetailsStudentSelectList"),
                courseListPlaceholder: $("#courseListPlaceholder"),
                studentListPlaceholder: $("#studentListPlaceholder")
            });

            // Do some page bootstrapping.
            Page.init();

            // Display course details for clicked course.
            $("#defaultPlaceholder").on("click", ".list-item", function (event) {
                var id = $(event.target).data("itemId");
                var toggleVisibility = $(event.target).hasClass("activeCourse");
                var editor = $(event.target).hasClass("glyphicon-edit");


                if(toggleVisibility){

                  var target = $(event.target);
                  $(target).siblings("a").slideToggle("slow");

                }
                // if the users push editbutton...
                if(editor){
                  Page.displayCourseDetails(id);
                }
                console.log("[#defaultPlaceholder.click]: Course list clicked: " + id);

                //Page.displayCourseDetails(id);
            });

            // Cancel the course details view.
            $("#courseDetailsCancelButton").on("click", function (event) {
                console.log("[#courseDetailsCancelButton.click]: Course details canceled.");

                // De-scelect the top menu button.
                Page.deselectMenu();

                Page.displayDefault();
            });

            // Save the course details.
            $("#courseDetailsForm").submit(function (event) {
                event.preventDefault();
                console.log("[courseDetailsForm.submit]: Submitted course details form.");

                var course = Utilities.formToJson(this);
                course.students = [];

                var student = null;
                $(".registered-student").each(function () {
                    student = {
                        id: $(this).data("id"),
                        firstName: $(this).data("firstName"),
                        lastName: $(this).data("lastName"),
                        personnummer: $(this).data("personnummer")
                    }
                    course.students.push(student);
                });

                Page.saveCourseAndDisplayDefault(course);
            });

            // Editmode in courselistTable
            $("#courseListTable").on("click", function () {
                var id = $(event.target).data("itemId");
                var active = $(event.target).data("itemAktiv");
                console.log("Edit mode: " + id);
                Page.activatCourseDetails(id);
                
            });

            $("#studentListTable").on("click", function(){
              var id = $(event.target).data("itemId");
              console.log("Edit mode: " + id);
              Page.displayStudentDetails(id);
            });
            // Remove a registered student.
            $("#courseDetailsStudentListPlaceholder").on("click", ".remove-registered-student", function (event) {
                var item = $(this).closest(".list-group-item")[0];

                // Append to the option list.
                var id = $(item).data("id");
                var firstName = $(item).data("firstName");
                var lastName = $(item).data("lastName");
                var student = { id: id, firstName: firstName, lastName: lastName }
                Page.appendStudentSelectOption(student);

                // Remove from the registered list.
                $(item).remove();
            });

            // Register a student.
            $("#registerSelectedStudentButton").on('click', function (event) {

                Page.registerSelectedStudent();

            });
            // Sets activ to the active bar.
            $('.navbar li, a').click(function (e) {
                $('.navbar li.active').removeClass('active');
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.addClass('active');
                }

                e.preventDefault();
            });

            $(".navigation").on("click", function () {
                var panel = this.href.substr(this.href.indexOf("#") + 1);

                console.log(panel);

                Page.navigate(panel);
            });

            // Save the new course details from the course list view.
            $("#courseListAddCourseForm").submit(function (event) {
                event.preventDefault();
                console.log("[courseListAddCourseForm.submit]: Submitted the new course form.");

                var course = Utilities.formToJson(this);
                course.students = [];
                $(this)[0].reset();

                Page.saveCourseDetails(course);
            });

            // Save the new student details from the student list view.
            $("#studentListAddCourseForm").submit(function (event) {
                event.preventDefault();
                console.log("[studentListAddCourseForm.submit]: Submitted the new Student form.");

                var student = Utilities.formToJson(this);
                student.students = [];
                $(this)[0].reset();

                Page.saveStudentDetails(student);
            });

            $(document).on("courseSavedCustomEvent", function (event) {
                console.log("[courseSavedCustomEvent]: " + event.message.description);
                console.log("[courseSavedCustomEvent]: " + event.message.data);

                Page.displayCourseList();

            });

            $(document).on("studentSavedCustomEvent", function (event) {
                console.log("[studentSavedCustomEvent]: " + event.message.description);
                console.log("[studentSavedCustomEvent]: " + event.message.data);

                Page.displayStudentList();

            });

        });
