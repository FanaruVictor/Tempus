using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Queries.Registrations.Download;

public class DownloadQueryHandler : IRequestHandler<DownloadQuery, BaseResponse<byte[]>>
{
    private readonly IRegistrationRepository _registrationRepository;

    public DownloadQueryHandler(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<BaseResponse<byte[]>> Handle(DownloadQuery request, CancellationToken cancellationToken)
    {
        BaseResponse<byte[]> result;

        var registration = await _registrationRepository.GetById(request.Id);
        if(registration == null)
        {
            result = BaseResponse<byte[]>.NotFound("registration not found");
            return result;
        }

        var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        writer.SetCloseStream(false);
        var document = new Document(pdf);
        var title = new Paragraph(registration?.Description)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20);
        var content = new Paragraph(registration?.Content)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetMargin(1.5f)
            .SetFontSize(15);

        document.Add(title);
        document.Add(content);
        document.Close();
        ms.Position = 0;

        result = BaseResponse<byte[]>.Ok(ms.GetBuffer());
        return result;
    }
}