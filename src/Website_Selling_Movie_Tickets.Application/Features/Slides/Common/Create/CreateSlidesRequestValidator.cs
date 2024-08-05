using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website_Selling_Movie_Tickets.Application.Features.Slides.Common.Create
{
    public class CreateSlidesRequestValidator : AbstractValidator<CreateSlidesRequest>
    {
        public CreateSlidesRequestValidator() 
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("Price is required")
                .MaximumLength(100).WithMessage("Price must be greater than 0");

            RuleFor(request => request.Url)
                .NotEmpty().WithMessage("Url is required")
                .Must(BeAValidUrl).WithMessage("Url must be a valid URL");

            RuleFor(request => request.Image)
                .NotEmpty().WithMessage("Image is required")
                .Must(BeAValidSize).WithMessage("Image must not exceed 100 KB")
                .Must(BeAValidFormat).WithMessage("Invalid image format. Only JPEG and PNG are allowed.");

            RuleFor(request => request.Sort)
                .NotEmpty().WithMessage("Sort is required")
                .GreaterThan(0).WithMessage("Sort must be greater than 0");

            RuleFor(request => request.Status)
                .NotEmpty().WithMessage("Status is required")
                .IsInEnum().WithMessage("Invalid status value");

        }
        //Check Url
        private bool BeAValidUrl(string url)
        {
            // Kiểm tra URL có hợp lệ và có sử dụng giao thức http hoặc https hay không
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result) &&
                   (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
        //Check Image
        private bool BeAValidSize(IFormFile file)
        {
            const int maxFileSizeInBytes = 100 * 1024; // 100 KB
            return file.Length <= maxFileSizeInBytes;
        }
        //Check Image
        private bool BeAValidFormat(IFormFile file)
        {
            var allowedFormats = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedFormats.Contains(fileExtension);
        }
    }
}
