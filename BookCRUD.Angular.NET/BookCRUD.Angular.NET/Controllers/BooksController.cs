using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model;
using RequestModel;
using Repository;
using ViewModel;

namespace BookCRUD.Angular.NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<BookViewModel>> GetAll()
        {
            var books = _repository.GetAll();
            return Ok(_mapper.Map<List<BookViewModel>>(books));
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookRequest request)

        {
            var book = _mapper.Map<Book>(request);
            _repository.Add(book);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookRequest request)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return NotFound();

            var updated = _mapper.Map(request, existing);
            _repository.Update(updated);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return NotFound();

            _repository.Delete(id);
            return Ok();
        }
    }
}
