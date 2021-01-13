    $(function () {

        function initVM() { return { movieIds: [] } }

            var vm = initVM();

            function getBloodhound({url}) {
                return new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    remote: {
        url: url,
                        wildcard: '%QUERY'
                    }
                });
            }

            function setupTypeAhead({inputName, name, source, callbackOn}) {
        $(inputName).typeahead({ minLength: 3, highlight: true }, {
            name: name, display: 'name', source: source
        }).on("typeahead:select", callbackOn);
            }

            function resetForm() {
        $("#customer").typeahead("val", "");
                $("#movie").typeahead("val", "");
                $("#movies").empty();
                vm = initVM();
                validator.resetForm();
            }

            function rentalCreationSuccess() {
        console.log("success");
                toastr.success("Rentals successfully recorded");
                resetForm();
            }

            var customers = getBloodhound({url: '/api/customers?query=%QUERY' });
            var movies = getBloodhound({url: '/api/movies?query=%QUERY' });

            setupTypeAhead({
        inputName: "#customer", name: "customer", source: customers, callbackOn:
                    function (e, customer) {
        vm.customerId = customer.id;
                        console.log('vm.customerId = ' + vm.customerId);
                    }
            });

            setupTypeAhead({
        inputName: "#movie", name: "movie", source: movies,
                callbackOn:
                    function (e, movie) {
        $("#movies").append("<li class='list-group-item'>" + movie.name + "</li>");
                        $("#movie").typeahead("val", "");
                        vm.movieIds.push(movie.id);
                        console.log('vm.movieId = ' + movie.id);
                        validator.resetForm();
                    }
            });

            $.validator.addMethod("validCustomer", function () {
                return vm.customerId && vm.customerId !== 0;
            }, "Please select a valid customer.");

            $.validator.addMethod("validMovie", function () {
                return vm.movieIds.length > 0;
            }, "Please select at least one movie.");

            var validator = $("#newRental").validate({
        rules: {
        customer: {validCustomer: true },
                    movie: {validMovie: true }
                },
                submitHandler: function () {
        $.post({
            url: "/api/newRentals",
            data: vm
        }).done(function () {
            rentalCreationSuccess();
        }).fail(function () {
            toastr.error("Rentals NOT recorded");
        });

                    // disable default form submit.
                    return false;
                }
            });
        });